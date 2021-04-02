using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.UI;

namespace SwitchAvoidance
{
    public class TSW10 : TouchPanel
    {
        Tsw1060 TP;
        public TSW10(string Descript, uint ID, ControlSystem cSystem)
        {
            this.Name = Descript;
            this.IPID = ID;
            Register(cSystem);
        }
        public void Register(ControlSystem cSystem)
        {
            TP = new Tsw1060(IPID, cSystem);
            this.Panel = TP;
            Panel.Description = this.Name;
            eDeviceRegistrationUnRegistrationResponse Response = Panel.Register();
            if (Response == eDeviceRegistrationUnRegistrationResponse.Success)
            {
                TP.ButtonStateChange += ButtonStateChange;
                Initialize();
            }
            else
            {
                ErrorLog.Error("{0} Panel Failed to Register: {1}", this.Name, Response.ToString());
            }
        }
    }
}