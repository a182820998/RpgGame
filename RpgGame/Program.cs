using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpgGame.GameDb;
using RpgGame.Presenter;

namespace RpgGame
{
    class Program : IView
    {
        public Program()
        {
            using (var system = new GameSystem(this))
            {
                system.StartGame();
            }
        }

        static void Main(string[] args)
        {
            Program myProgram = new Program();
        }

        #region IView implementation
        public string InputQuery()
        {
            return Console.ReadLine();
        }

        public void PrintMain()
        {
            Console.WriteLine("1. 創建角色");
            Console.WriteLine("2. 角色作戰");
            Console.WriteLine("3. 列出所有角色");
            Console.WriteLine("4. 刪除角色");
            Console.WriteLine("5. 新增職業");
            Console.WriteLine("6. 刪除職業");
            Console.WriteLine("7. 離開");
        }

        public void PrintToMuchCharacters()
        {
            Console.WriteLine("角色數量已達上限！");
            Console.WriteLine(String.Empty);
        }

        public void PrintNoCharacters()
        {
            Console.WriteLine("目前無角色可選擇！");
            Console.WriteLine(String.Empty);
        }

        public void PrintInformation(string name, string jobName, int level, string attribute,
            int exp, int str, string extraStr, int dex, string extraDex, int con, string extraCon, int totalFightTimes,
            int failFightTimes)
        {
            Console.WriteLine("角色名稱 : {0}", name);
            Console.WriteLine("目前等級 : {0} (職業 : {1} 所屬 : {2}系)", level, jobName, attribute);
            Console.WriteLine("目前經驗值 : {0}", exp);
            Console.WriteLine("STR : {0}{1}", str, extraStr);
            Console.WriteLine("DEX : {0}{1}", dex, extraDex);
            Console.WriteLine("CON : {0}{1}", con, extraCon);
            Console.WriteLine("總戰鬥場數 : {0}", totalFightTimes);
            Console.WriteLine("戰鬥失敗場數 : {0}", failFightTimes);
            Console.WriteLine(String.Empty);
        }

        public void PrintErrorInput()
        {
            Console.WriteLine("指令錯誤，請重新輸入。");
            Console.WriteLine(String.Empty);
        }

        public void PrintNameInput()
        {
            Console.Write("請輸入名稱 : ");
        }

        public void PrintIllegelName()
        {
            Console.WriteLine("名稱不可空白或帶有特殊符號。");
            Console.WriteLine(String.Empty);
        }

        public void PrintCharacterInit(int str, int dex, int con, int point)
        {
            Console.WriteLine("請選擇屬性 : ");
            Console.WriteLine("1.目前STR : {0}", str);
            Console.WriteLine("2.目前DEX : {0}", dex);
            Console.WriteLine("3.目前CON : {0}", con);
            Console.WriteLine("目前剩餘點數 : {0}", point);
            Console.WriteLine(String.Empty);
        }

        public void PrintJobAttribute()
        {
            Console.WriteLine("請選擇職業種類 : ");
            Console.WriteLine("1. 近距離戰鬥系");
            Console.WriteLine("2. 遠距離戰鬥系");
            Console.WriteLine("3. 防禦系");
            Console.WriteLine(String.Empty);
        }

        public void PrintJobName(int count, string jobName)
        {
            Console.WriteLine("{0}. {1}", count, jobName);
        }

        public void PrintNoThisTypeJob()
        {
            Console.WriteLine("此屬性目前無職業，請重新選擇 : ");
        }

        public void PrintCreateSuccess()
        {
            Console.WriteLine("創建成功！");
            Console.WriteLine(String.Empty);
        }

        public void PrintNameChoice(int count, string name)
        {
            Console.WriteLine("{0}. {1}", count, name);
        }

        public void PrintBackToMain()
        {
            Console.WriteLine("取消，返回主選單！");
            Console.WriteLine(String.Empty);
        }

        public void PrintStartFight(string characterName, string enemyName, string enemyJobName, int characterDex,
            int enemyDex)
        {
            Console.WriteLine("系統選擇了「{0}」作為敵人", enemyName);
            Console.WriteLine("系統選擇了「{0}」作為{1}的職業", enemyJobName, enemyName);
            Console.WriteLine("{0}的DEX為 : {1}，{2}的DEX為 : {3}，由{4}先進行攻擊", characterName, characterDex, enemyName, enemyDex,
                (characterDex < enemyDex) ? enemyName : characterName);
            Console.WriteLine(String.Empty);
        }

        public void PrintFightLive(string attackName, string defenseName, int damage, string antagonistic,
            int preAttackHp, int newHp)
        {
            Console.WriteLine("{0}發動攻擊！對{1}造成{2}點傷害。{3}", attackName, defenseName, damage, antagonistic);
            Console.WriteLine("{0}的HP從{1}變為{2}。", defenseName, preAttackHp, newHp);
            Console.WriteLine(String.Empty);
        }

        public void PrintFightResult(string winnerName, string loserName)
        {
            Console.WriteLine("{0}倒下了，獲勝的是{1}", loserName, winnerName);
            Console.WriteLine(String.Empty);
        }

        public void PrintUpgradeNotice()
        {
            Console.WriteLine("角色升級了！獲得新的3點點數可以分配到當前屬性上 : ");
        }

        public void PrintUpgradeMenu(int str, string extraStr, int dex, string extraDex, int con, string extraCon,
            int point)
        {
            Console.WriteLine(String.Empty);
            Console.WriteLine("1.目前STR : {0}{1}", str, extraStr);
            Console.WriteLine("1.目前DEX : {0}{1}", dex, extraDex);
            Console.WriteLine("1.目前CON : {0}{1}", con, extraCon);
            Console.WriteLine("目前剩餘點數 : {0}", point);
            Console.Write("請選擇欲增加的屬性 : ");
        }

        public void PrintUpgradeResult(string characterName, int level)
        {
            Console.WriteLine("配點完成，{0}的等級達到{1}", characterName, level);
            Console.WriteLine(String.Empty);
        }

        public void PrintDeletedNotice(string title)
        {
            Console.Write("請選擇欲刪除的{0} : ", title);
        }

        public void PrintDeleteSuccess(string title, string name)
        {
            Console.WriteLine("已刪除{0} : {1}", title, name);
            Console.WriteLine(String.Empty);
        }

        public void PrintExtraNotice(string extra)
        {
            Console.Write("請設定{0}額外加成 : ", extra);
        }

        void IView.PrintIllegelExtraValue()
        {
            Console.WriteLine("額外加成數值超過3，請重新輸入。");
            Console.WriteLine(String.Empty);
        }

        public void PrintDeleteJobDeny(string text)
        {
            Console.WriteLine("刪除失敗，{0}", text);
            Console.WriteLine(String.Empty);
        }
        #endregion
    }
}
