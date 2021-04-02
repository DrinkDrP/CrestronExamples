using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.UI;

using System;
using System.Collections.Generic;

using SwitchAvoidance.Joins;

namespace SwitchAvoidance
{
    public class TouchPanel
    {
        /// <summary>
        /// Dictionary storing look ups for all our touchpanel presses
        /// This can be extrapolated to include SmartGraphicObject changes as well
        /// This simple demo is just for digital joins.
        /// </summary>
        protected Dictionary<uint, Action<bool>> Digitals;
        protected Dictionary<uint, Action<ushort>> Analogs;
        protected Dictionary<uint, Action<string>> Serials;

        
        public TouchPanel() {
        /* Here we can initialize our dictionary. 
         * We could also do this on class generation but if you've got a 
         * duplicate key, it will cause the program to crash and not start.
         * Which can have you pulling your hair out.
         * 
         * For the simple example, I'm only populating Digitals
         */

            //Action<bool> is just a delegate holder for the method we want called
            //if you need the call to return a value, use Func<bool, object> with object being the return value;

            Digitals = new Dictionary<uint, Action<bool>>() {
                { (uint)DigitalJoins.VolumeUp, VolumeUp },
                { (uint)DigitalJoins.VolumeDown, VolumeDown },
                { (uint)DigitalJoins.Mute, VolumeMute },
                { (uint)DigitalJoins.SourceAppleTV, SourceAppleTV }
                //etc, you get the point
            };
        }

        public bool isInitialized = false;

        public uint IPID { get; set; }
        public string Name { get; set; }

        protected void Initialize()
        {
            try
            {
                Panel.BaseEvent += BaseEvent;
                Panel.OnlineStatusChange += OnlineStatusChange;
                Panel.IpInformationChange += IpInformationChange;
                Panel.SigChange += SigChange;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("Error in {0} Panel RegisterSmartObjects(), message: {1}", Name, ex.Message);
            }
        }

        public BasicTriListWithSmartObject Panel { get; set; }

        private void SigChange(BasicTriList device, SigEventArgs args)
        {
            //Yes I know this is a switch. 
            //But its a quick and simple one
            switch (args.Sig.Type)
            {
                case eSigType.Bool:
                    {
                        Action<bool> action;
                        if (Digitals.TryGetValue(args.Sig.Number, out action))
                        {
                            action.Invoke(args.Sig.BoolValue);
                            return;
                        }
                        break;
                    }
                case eSigType.UShort:
                    {
                        break;
                    }
                case eSigType.String:
                    {
                        break;
                    }
            }

            /* If we get down here, it means that we saw a signal that isn't being handled by our dictionaries. 
             * Could be a bad thing, but more than likely not.
             */

        }
        private void BaseEvent(GenericBase device, BaseEventArgs args)
        {
        }
        protected void ButtonStateChange(GenericBase device, ButtonEventArgs args)
        {            
        }
        private void OnlineStatusChange(GenericBase device, OnlineOfflineEventArgs args)
        {
        }
        private void IpInformationChange(GenericBase device, ConnectedIpEventArgs args)
        {
        }


        #region Digital Methods
        private void VolumeUp(bool value)
        {
            CrestronConsole.PrintLine("Got Volume Up value: {0}", value.ToString());
        }
        private void VolumeDown(bool value)
        {
            CrestronConsole.PrintLine("Got Volume Down value: {0}", value.ToString());
        }
        private void VolumeMute(bool value)
        {
            CrestronConsole.PrintLine("Got Volume Mute value: {0}", value.ToString());
        }
        private void SourceAppleTV(bool value)
        {
            CrestronConsole.PrintLine("Got Source Apple TV value: {0}", value.ToString());
        }
        #endregion
    }
}