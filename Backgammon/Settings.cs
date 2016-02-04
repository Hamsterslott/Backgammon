using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
	public static class Settings
	{
		internal static bool playSound = true;
        internal static bool isSongPlaying = false;
        internal static bool helpActive = true;
		internal static int [] playerTheme = new int [2]{1,0};
		internal static int [] backgroundpos = new int []{0,1,2,3,4};
        internal static int background = backgroundpos[0];
		internal static bool ShowSplash = false;
	}
}
