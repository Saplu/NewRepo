using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionClassLibrary
{
    public class MissionStatus
    {
        private int turn;
        private int selectedPlayerPosition;
        private string skillID;
        private int targetSide;
        private List<int> actionDone;
        private int[] cdarr;

        public int Turn { get => turn; set => turn = value; }
        public int SelectedPlayerPosition { get => selectedPlayerPosition; set => selectedPlayerPosition = value; }
        public string SkillID { get => skillID; set => skillID = value; }
        public int TargetSide { get => targetSide; set => targetSide = value; }
        public List<int> ActionDone { get => actionDone; set => actionDone = value; }
        public int[] Cdarr { get => cdarr; set => cdarr = value; }

        public MissionStatus()
        {
            turn = 1;
            selectedPlayerPosition = 0;
            skillID = "";
            targetSide = 0;
            actionDone = new List<int>();
            cdarr = new int[4];
        }

        public void EndTurn(List<int> deadPlayers)
        {
            turn++;
            selectedPlayerPosition = 0;
            skillID = "";
            targetSide = 0;
            actionDone.Clear();
            actionDone.AddRange(deadPlayers);
            cdarr = new int[4];
        }

        public void SetSkillID(string id)
        {
            skillID = id;
            if (Utils.HealAbilities.IsHeal(id))
                targetSide = 1;
            else targetSide = 2;
        }
    }
}
