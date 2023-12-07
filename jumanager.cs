using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsAccessBridgeInterop;

namespace umanagercontroller
{
    internal class jumanager
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        AccessBridge java = new AccessBridge();
        // use one pedal to iterate over all options.
        int current_active_button = 0;
        List<AccessibleNode> buttons = new List<AccessibleNode>();
        //for the live buttons
        AccessibleNode live = null;
        JavaObjectHandle jLive = null;
        //for the preset buttons.
        List<JavaObjectHandle> jHandles = new List<JavaObjectHandle>();
        List<string> botton_text = new List<string>();

        //Go over all the object on the java form and extract the buttons. Also identify which is the
        // live button and have a special case for it.
        private void find_all_buttons(IEnumerable<AccessibleNode> children, JavaObjectHandle parentHandle)
        {
            JavaObjectHandle childHandle = null;
            foreach (AccessibleNode child in children)
            {
                string s = child.GetTitle();
                childHandle = java.Functions.GetAccessibleChildFromContext(child.JvmId, parentHandle, child.GetIndexInParent());
                if (s.Contains("push button"))
                {
                    string button_caption = s.Replace("push button: ", "");
                    // if the user activated imaging then it will be in "Stop Live" mode.
                    if (button_caption == "Live" || button_caption == "Stop Live")
                    {
                        live = child;
                        jLive = childHandle;
                    }
                    else
                    {
                        buttons.Add(child);
                        jHandles.Add(childHandle);
                    }
                }
                find_all_buttons(child.GetChildren(), childHandle);
            }
        }

        public jumanager()
        {
            java.Initialize();
            //A hack to wait for Java.Initialize to complete(My simple understanding is Java Access Bridge Uses a hidden window or similar)
            Application.DoEvents();
            //Get the handle of the Java Window with WINAPI32
            IntPtr wHandle = FindWindow("SunAwtFrame", "Pedal Control");
            AccessibleWindow win = java.CreateAccessibleWindow(wHandle);
            //Get Access to the Java Object that represents the main window.
            java.Functions.GetAccessibleContextFromHWND(wHandle, out int vmid, out JavaObjectHandle jwindowHandle);
            IEnumerable<AccessibleNode> children = win.GetChildren();
            //then find all the children nodes which are buttons - can do it with other note types as well.
            // Importantly here you can use the program "AccessBridgeExplorer.exe" to find all the details 
            // for the java windows and components. Do it recursively here.
            find_all_buttons(children, jwindowHandle);
        }

        public string click()
        {
            //Get Possible Actions
            current_active_button = (current_active_button + 1) % 3;
            AccessibleNode b = buttons[current_active_button];
            JavaObjectHandle j = jHandles[current_active_button];
            java.Functions.GetAccessibleActions(b.JvmId, j, out AccessibleActions accessibleActions);
            
            //not sure why eventually there is only one action and it is the click action.
            // But it seem to work find. So leave it at that.
            AccessibleActionsToDo accessibleActionsToDo = new AccessibleActionsToDo();
            accessibleActionsToDo.actions = accessibleActions.actionInfo;
            accessibleActionsToDo.actionsCount = accessibleActions.actionsCount;

            //Do Actions
            live.Refresh(); // to get the right caption on the button when it changes.
            if (live.GetTitle().Replace("push button: ", "") == "Stop Live")
            {
                java.Functions.DoAccessibleActions(live.JvmId, jLive, ref accessibleActionsToDo, out int failure);
                java.Functions.DoAccessibleActions(b.JvmId, j, ref accessibleActionsToDo, out failure);
                java.Functions.DoAccessibleActions(live.JvmId, jLive, ref accessibleActionsToDo, out failure);
            }
            else
            {
                java.Functions.DoAccessibleActions(b.JvmId, j, ref accessibleActionsToDo, out int failure);
            }
            return b.GetTitle();

        }
    }
}
