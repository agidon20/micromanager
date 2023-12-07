using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsAccessBridgeInterop;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


// must enable that:
// %JRE_HOME%\bin\jabswitch -enable 
// in the directory of micromanager

namespace umanagercontroller
{
    public partial class Form1 : Form
    {
        jumanager jum;
        public Form1()
        {

            InitializeComponent();
            jum = new jumanager();
        }

        //To Interact with an object(eg click a button), do similar to the following:
        //(please note where it says JavaObject - it means the child java object
        //(eg.to click a button you need to get the JavaObject for that
        //button using GetAccessibleChildFromContext as i mentioned above)
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = jum.click();
            
        }
  
    }
}
