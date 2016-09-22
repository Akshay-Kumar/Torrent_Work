package net.filebot.ui.transfer;

import static net.filebot.Logging.*;
import static net.filebot.Settings.*;
import static net.filebot.util.FileUtilities.*;

import java.awt.datatransfer.DataFlavor;
import java.awt.datatransfer.Transferable;
import java.awt.datatransfer.UnsupportedFlavorException;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.net.URI;
import java.nio.CharBuffer;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.List;
import java.util.Scanner;

import net.filebot.gio.GVFS;

public class FileTransferable implements Transferable {

	public static final DataFlavor uriListFlavor = createUriListFlavor();

	private static DataFlavor createUriListFlavor() {
		try {
			return new DataFlavor("text/uri-list;class=java.nio.CharBuffer");
		} catch (ClassNotFoundException e) {
			// will never happen
			throw new RuntimeException(e);
		}
	}

	private final File[] files;

	public FileTransferable(File... files) {
		this.files = files;
	}

	public FileTransferable(Collection<File> files) {
		this.files = files.toArray(new File[0]);
	}

	@Override
	public Object getTransferData(DataFlavor flavor) throws UnsupportedFlavorException {
		if (flavor.isFlavorJavaFileListType())
			return Arrays.asList(files);
		else if (flavor.equals(uriListFlavor))
			return CharBuffer.wrap(getUriList());
		else
			throw new UnsupportedFlavorException(flavor);
	}

	/**
	 * @return line separated list of file URIs
	 */
	private String getUriList() {
		StringBuilder sb = new StringBuilder(80 * files.length);

		for (File file : files) {
			// use URI encoded path
			sb.append("file://").append(file.toURI().getRawPath());
			sb.append("\r\n");
		}

		return sb.toString();
	}

	@Override
	public DataFlavor[] getTransferDataFlavors() {
		return new DataFlavor[] { DataFlavor.javaFileListFlavor, uriListFlavor };
	}

	@Override
	public boolean isDataFlavorSupported(DataFlavor flavor) {
		return isFileListFlavor(flavor);
	}

	public static boolean isFileListFlavor(DataFlavor flavor) {
		return flavor.isFlavorJavaFileListType() || flavor.equals(uriListFlavor);
	}

	public static boolean hasFileListFlavor(Transferable tr) {
		return tr.isDataFlavorSupported(DataFlavor.javaFileListFlavor) || tr.isDataFlavorSupported(FileTransferable.uriListFlavor);
	}

	@SuppressWarnings("unchecked")
	public static List<File> getFilesFromTransferable(Transferable tr) throws IOException, UnsupportedFlavorException {
		// On Linux, if a file is dragged from a smb share to into a java application (e.g. Ubuntu Files to FileBot)
		// the application/x-java-file-list transfer data will be an empty list
		// because the native uri-list flavor drop data will be a list of smb URLs (e.g. smb://10.0.0.1/data/file.txt)
		// and not local GVFS paths (e.g. /run/user/1000/gvfs/smb-share:server=10.0.01.1,share=data/file.txt)
		if (useGVFS()) {
			if (tr.isDataFlavorSupported(FileTransferable.uriListFlavor)) {
				// file URI list flavor (Linux)
				Readable transferData = (Readable) tr.getTransferData(FileTransferable.uriListFlavor);
				Scanner scanner = new Scanner(transferData);
				List<File> files = new ArrayList<File>();

				while (scanner.hasNextLine()) {
					String line = scanner.nextLine();

					if (line.startsWith("#")) {
						// the line is a comment (as per RFC 2483)
						continue;
					}

					try {
						URI uri = new URI(line);
						File file = GVFS.getDefaultVFS().getPathForURI(uri);

						if (file == null || !file.exists()) {
							throw new FileNotFoundException(line);
						}

						files.add(file);
					} catch (Throwable e) {
						// URISyntaxException, IllegalArgumentException, FileNotFoundException, LinkageError, etc
						debug.warning("Invalid file URI: " + line);
					}
				}

				return sortByUniquePath(files); // FORCE NATURAL FILE ORDER
			}
		}

		// Windows, Mac and default handling
		if (tr.isDataFlavorSupported(DataFlavor.javaFileListFlavor)) {
			// file list flavor
			Object transferable = tr.getTransferData(DataFlavor.javaFileListFlavor);
			if (transferable instanceof List) {
				return sortByUniquePath((List<File>) transferable); // FORCE NATURAL FILE ORDER
			} else {
				return null; // on some platforms transferable data will not be available until the drop has been accepted
			}
		}

		// cannot get files from transferable
		throw new UnsupportedFlavorException(DataFlavor.javaFileListFlavor);
	}
}
