/* Quest settings:
    * Can condition: "return Usefuls.ContinentId == (int) ContinentId.Kalimdor;"
    * Is complete condition: "return Usefuls.ContinentId == (int) ContinentId.Kalimdor && !ObjectManager.Me.InTransport;"
    * Not required in quest log: "True"
    * Quest type: "OverridePulseCSharpCode"
*/

// You can get zeppelin/ship/player positions and entry ID in tab "Tools" > "Development Tools" > "Dump all informations" (or "Memory information").

// Settings:
var zeppelinEntryId = 176310; // Zeppelin/Ship EntryId
// From
var fromZeppelinWaitPosition = new Vector3(-8650.7, 1346.1, 0); // Position where Zeppelin/Ship waits players (from)
var fromPlayerWaitPosition = new Vector3(-8643.6, 1333.8, 5.6); // Position where the player waits Zeppelin/Ship (from)
var fromPlayerInZeppelinPosition = new Vector3(-8645.4, 1338.6, 6.1); // Position where the player waits in the Zeppelin/Ship (from)
// To
var toZeppelinWaitPosition = new Vector3(6406, 822.9, 0); // Position where Zeppelin/Ship waits players (to)
var toPlayerLeavePosition = new Vector3(6420.6, 821.1, 5.8); // Position to go out the Zeppelin/Ship (to)
//Pos1
var enterPos1 = new Vector3(-8643.6, 1333.8, 5.6);
//Pos2
var enterPos2 = new Vector3(-8645.4, 1338.6, 6.1);

// Change WRobot settings:
wManager.wManagerSetting.CurrentSetting.CloseIfPlayerTeleported = false;
wManager.wManagerSetting.CurrentSetting.Selling = false;
wManager.wManagerSetting.CurrentSetting.Repair = false;

// Code:
if (!Conditions.InGameAndConnectedAndProductStartedNotInPause)
    return true;

if (Usefuls.ContinentId != (int)ContinentId.Kalimdor)
{
    if (!ObjectManager.Me.InTransport)
    {
        if (GoToTask.ToPosition(fromPlayerWaitPosition))
        {
            var zeppelin = ObjectManager.GetWoWGameObjectByEntry(zeppelinEntryId).OrderBy(o => o.GetDistance).FirstOrDefault();
            if (zeppelin != null && zeppelin.Position.DistanceTo(fromZeppelinWaitPosition) < 1)
            {

 GoToTask.ToPosition(enterPos1);
if(GoToTask.ToPosition(enterPos1))
GoToTask.ToPosition(enterPos2);

Lua.LuaDoString("ClearTarget()");

wManager.Wow.Helpers.MovementManager.Face(new Vector3(-8645.4, 1338.6, 6.1));

while(fromPlayerInZeppelinPosition.DistanceTo(ObjectManager.Me.Position) > 3)
{
wManager.Wow.Helpers.Move.Forward(Move.MoveAction.PressKey, 250);
      Thread.Sleep(robotManager.Helpful.Others.Random(25, 50));
}
               GoToTask.ToPosition(fromPlayerInZeppelinPosition);
            }
        }
    }
}
else if (Usefuls.ContinentId == (int)ContinentId.Kalimdor)
{
    if (ObjectManager.Me.InTransport)
    {
        var zeppelin = ObjectManager.GetWoWGameObjectByEntry(zeppelinEntryId).OrderBy(o => o.GetDistance).FirstOrDefault();
        if (zeppelin != null && zeppelin.Position.DistanceTo(toZeppelinWaitPosition) < 1)
        {
            wManager.Wow.Helpers.Move.Forward(Move.MoveAction.PressKey, 1500);
            wManager.Wow.Helpers.Move.StrafeRight(Move.MoveAction.PressKey, 1000);
            wManager.Wow.Helpers.Move.Forward(Move.MoveAction.PressKey, 1200);

            GoToTask.ToPosition(toPlayerLeavePosition);
            wManager.wManagerSetting.CurrentSetting.CloseIfPlayerTeleported = true;
wManager.wManagerSetting.CurrentSetting.Selling = true;
wManager.wManagerSetting.CurrentSetting.Repair = true;
wManager.Wow.Forms.UserControlTabGeneralSettings.ReloadGeneralSettings();
        }
    }
}
return true;