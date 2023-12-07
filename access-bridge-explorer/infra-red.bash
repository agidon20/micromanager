import ij.IJ;
import ij.ImagePlus;
import ij.ImageWindow;
import ij.ImageCanvas;
import ij.gui.ImageWindow;
import java.awt.GraphicsDevice;
import java.awt.GraphicsEnvironment;
import org.micromanager.display;
import java.awt.Component;



live = mm.live();
boolean live_mode_on = live.isLiveModeOn();
if(live_mode_on) live.setLiveModeOn(false);
mm.core().setConfig("Channel", "FITC");
mm.core().waitForConfig("Channel","FITC");
mm.app().refreshGUI();
arrange_camera_panel();
if(live_mode_on) live.setLiveModeOn(true);

