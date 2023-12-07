import ij.IJ;
import ij.ImagePlus;
import ij.ImageWindow;
import ij.ImageCanvas;
import ij.gui.ImageWindow;
import java.awt.GraphicsDevice;
import java.awt.GraphicsEnvironment;
import org.micromanager.display;
import java.awt.Component;

//https://imagej.net/ij/developer/api/allclasses.html
//https://valelab4.ucsf.edu/~MM/doc-2.0.0-beta/mmstudio/allclasses-noframe.html
//https://micro-manager.org/apidoc/mmcorej/latest/mmcorej/CMMCore.html
// use inspect(obj) to see all properties.

//code to run on mmStartup.bsh
//void arrange_camera_panel(){
//	GraphicsEnvironment env = GraphicsEnvironment.getLocalGraphicsEnvironment();
//	GraphicsDevice[] devices = env.getScreenDevices();
//
//	// Windows scaled sizes (eg 1280x720 for my case at 150% scaling)
//	Rectangle bounds = devices[0].getDefaultConfiguration().getBounds();
//	// Display sizes (same as above at 100% scale, 1920x1080 for my case)
//	DisplayMode dm = devices[0].getDefaultConfiguration().getDevice().getDisplayMode();
//	Rectangle orig = new Rectangle((int)bounds.getX(), (int)bounds.getY(), dm.getWidth(), dm.getHeight());
//	String binning = mm.core().getProperty("Camera","Binning");
//	Integer bin = Integer.parseInt(binning);
//	//win = IJ.getImage().getWindow();
//	live = mm.live();
//	disp = live.getDisplay();
//	boolean live_mode_on = live.isLiveModeOn();
//	if(live_mode_on) live.setLiveModeOn(false);
//	if(disp != null){
//		disp.setZoom(3.7*bin);
//		live_win = disp.getWindow();
//		live_win.setMaximizedBounds(env.getMaximumWindowBounds());
//		live_win.setExtendedState(live_win.getExtendedState() | live_win.MAXIMIZED_BOTH);
//		//live_win.setBounds(orig);
//		//live_win.toFront();	
//	}
//	if(live_mode_on) live.setLiveModeOn(true);
//	//mm.core().setConfig("Channel", "FITC");
//	//mm.app().refreshGUI();	
//}
arrange_camera_panel();
