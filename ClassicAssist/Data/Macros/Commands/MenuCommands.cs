﻿#region License

// Copyright (C) 2020 Reetus
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using Assistant;
using ClassicAssist.Resources;
using ClassicAssist.UO.Network.Packets;
using ClassicAssist.UO.Objects;
using UOC = ClassicAssist.UO.Commands;

namespace ClassicAssist.Data.Macros.Commands
{
    public static class MenuCommands
    {
        [CommandsDisplay( Category = nameof( Strings.Menus ),
            Parameters = new[]
            {
                nameof( ParameterType.ItemID ), nameof( ParameterType.Timeout ), nameof( ParameterType.Boolean )
            } )]
        public static bool WaitForMenu( int gumpId = 0, int timeout = 30000, bool filter = true )
        {
            bool result = UOC.WaitForMenu( gumpId, timeout, filter );

            if ( !result )
            {
                UOC.SystemMessage( Strings.Timeout___ );
            }

            return result;
        }

        [CommandsDisplay( Category = nameof( Strings.Menus ),
            Parameters = new[]
            {
                nameof( ParameterType.ItemID ), nameof( ParameterType.GumpButtonIndex ),
                nameof( ParameterType.ItemID ), nameof( ParameterType.Hue )
            } )]
        public static void ReplyMenu( int gumpId, int buttonId, int itemId = 0, int hue = 0 )
        {
            Engine.SendPacketToServer( new MenuButtonClick( gumpId, -1, buttonId, itemId, hue ) );
        }

        [CommandsDisplay( Category = nameof( Strings.Menus ),
            Parameters = new[] { nameof( ParameterType.ItemID ), nameof( ParameterType.String ) } )]
        public static bool InMenu( int gumpId, string text )
        {
            text = text.ToLower();

            if ( !Engine.Menus.GetMenu( gumpId, out Menu menu ) )
            {
                return false;
            }

            if ( menu.Title.ToLower().Contains( text ) )
            {
                return true;
            }

            bool? any = false;

            foreach ( MenuEntry l in menu.Lines )
            {
                if ( l.Title.ToLower().Contains( text ) )
                {
                    any = true;
                    break;
                }
            }

            return any ?? false;
        }

        [CommandsDisplay( Category = nameof( Strings.Menus ) )]
        public static bool MenuExists( int gumpId )
        {
            return Engine.Menus.GetMenu( gumpId, out _ );
        }
    }
}