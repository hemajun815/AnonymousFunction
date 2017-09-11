using System;
using System.Collections.Generic;
using System.Linq;

namespace WebHelper
{
    /// <summary>
    /// 网页在线播放
    /// 2015-10-24 14:10 hemajun
    /// </summary>
    public class HelperWebMediaPlay
    {
        /// <summary>
        /// 播放Flash
        /// </summary>
        /// <returns>Html</returns>
        public static String PlayFlash(String url, String width, String height)
        {
            System.Text.StringBuilder inner = new System.Text.StringBuilder();
            inner.AppendFormat("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' width='{0}' height='{1}' xcodebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab'>", width, height);
            inner.AppendFormat("<param name='Movie' value='{0}' />", url);
            inner.Append("<param name='wmode' value='transparent' />");
            inner.AppendFormat("<embed src='{0}' width='{1}' height='{2}' quality='high' wmode='transparent'  type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer'></embed>", url, width, height);
            inner.Append("</object>");
            return inner.ToString();
        }

        /// <summary>
        /// 播放Video
        /// </summary>
        /// <returns>Html</returns>
        public static String PlayVideo(String url, String width, String height)
        {
            System.Text.StringBuilder inner = new System.Text.StringBuilder();
            inner.AppendFormat("<object id='MediaPlayer1' classid='clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95' width='{0}' height='{1}'>", width, height);
            inner.AppendFormat("<param name='filename' value='{0}'/>", url);
            inner.Append("<param name='AutoStart' value='-1'/>");
            inner.Append("<param name='Enabled' value='-1'/>");
            inner.Append("<param name='ShowStatusBar' value='0'/>");
            inner.Append("<param name='ShowControls' value='-1'/>");
            inner.Append("<param name='ShowGotoBar' value='0'/>");
            inner.Append("<param name='EnableFullScreenControls' value='0'/>");
            inner.Append("<param name='EnablePositionControls' value='0'/>");
            inner.Append("<param name='Volume' value='0'/>");
            inner.Append("<param name='DisplaySize' value='4'/>");
            inner.Append("<param name='SendErrorEvents' value='0'/>");
            inner.Append("<param name='enableContextMenu' value='0'/>");
            inner.Append("<param name='EnableTracker' value='-1'/>");
            inner.Append("<param name='AudioStream' value='-1'/>");
            inner.Append("<param name='AutoSize' value='0'/>");
            inner.Append("<param name='AnimationAtStart' value='-1'/>");
            inner.Append("<param name='AllowScan' value='-1'/>");
            inner.Append("<param name='AllowChangeDisplaySize' value='-1'/>");
            inner.Append("<param name='AutoRewind' value='0'/>");
            inner.Append("<param name='Balance' value='0'/>");
            inner.Append("<param name='BaseURL' value=''/>");
            inner.Append("<param name='BufferingTime' value='5'/>");
            inner.Append("<param name='CaptioningID' value=''/>");
            inner.Append("<param name='ClickToPlay' value='-1'/>");
            inner.Append("<param name='CursorType' value='0'/>");
            inner.Append("<param name='CurrentPosition' value='-1'/>");
            inner.Append("<param name='CurrentMarker' value='0'/>");
            inner.Append("<param name='DefaultFrame' value=''/> ");
            inner.Append("<param name='DisplayBackColor' value='0'/>");
            inner.Append("<param name='DisplayForeColor' value='16777215'/>");
            inner.Append("<param name='DisplayMode' value='0'/> ");
            inner.Append("<param name='InvokeURLs' value='-1'/>");
            inner.Append("<param name='Language' value='-1'/>");
            inner.Append("<param name='Mute' value='0'/>");
            inner.Append("<param name='PlayCount' value='1'/>");
            inner.Append("<param name='PreviewMode' value='0'/>");
            inner.Append("<param name='Rate' value='1'/>");
            inner.Append("<param name='SAMILang' value=''/>");
            inner.Append("<param name='SAMIStyle' value=''/>");
            inner.Append("<param name='SAMIFileName' value=''/>");
            inner.Append("<param name='SelectionStart' value='-1'/>");
            inner.Append("<param name='SelectionEnd' value='-1'/>");
            inner.Append("<param name='SendOpenStateChangeEvents' value='-1'/>");
            inner.Append("<param name='SendWarningEvents' value='-1'/>");
            inner.Append("<param name='SendKeyboardEvents' value='0'/>");
            inner.Append("<param name='SendMouseClickEvents' value='0'/>");
            inner.Append("<param name='SendMouseMoveEvents' value='0'/>");
            inner.Append("<param name='SendPlayStateChangeEvents' value='-1'/>");
            inner.Append("<param name='ShowCaptioning' value='0'/>");
            inner.Append("<param name='ShowAudioControls' value='-1'/>");
            inner.Append("<param name='ShowDisplay' value='0'/>");
            inner.Append("<param name='ShowPositionControls' value='-1'/>");
            inner.Append("<param name='ShowTracker' value='-1'/>");
            inner.Append("<param name='TransparentAtStart' value='0'/>");
            inner.Append("<param name='VideoBorderWidth' value='0'/>");
            inner.Append("<param name='VideoBorderColor' value='0'/>");
            inner.Append("<param name='VideoBorder3D' value='0'/>");
            inner.Append("<param name='WindowlessVideo' value='0'/>");
            inner.AppendFormat("<embed name='MediaPlayer1' src='{0}' width='{1}' height='{2}' pluginspage='http://www.microsoft.com/Windows/MediaPlayer' type='application/x-mplayer2' autostart='1' showcontrols='1'  > ", url, width, height);
            inner.Append("</object>");
            return inner.ToString();
        }
    }
}
