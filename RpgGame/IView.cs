using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    interface IView
    {
        string InputQuery();
        void PrintMain();
        void PrintToMuchCharacters();
        void PrintNoCharacters();
        void PrintInformation(string name, string jobName, int level, string attribute, int exp, int str,
            string extraStr, int dex,
            string extraDex, int con, string extraCon, int totalFightTimes, int failFightTimes);
        void PrintErrorInput();
        void PrintNameInput();
        void PrintIllegelName();
        void PrintCharacterInit(int str, int dex, int con, int point);
        void PrintJobAttribute();
        void PrintJobName(int count, string jobName);
        void PrintNoThisTypeJob();
        void PrintCreateSuccess();
        void PrintNameChoice(int count, string characterName);
        void PrintBackToMain();
        void PrintStartFight(string characterName, string enemyName, string enemyJobName, int characterDex, int enemyDex);
        void PrintFightLive(string attackName, string defenseName, int damage, string antagonistic, int preAttackHp, int newHp);
        void PrintFightResult(string winnerName, string loserName);
        void PrintUpgradeNotice();
        void PrintUpgradeMenu(int str, string extraStr, int dex, string extraDex, int con, string extraCon, int point);
        void PrintUpgradeResult(string characterName, int level);
        void PrintDeletedNotice(string title);
        void PrintDeleteSuccess(string title, string name);
        void PrintExtraNotice(string extra);
        void PrintIllegelExtraValue();
        void PrintDeleteJobDeny(string text);
    }
}
