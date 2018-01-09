using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using RpgGame.GameDb;
using RpgGame.Model.GameCharacter;
using RpgGame.Model.GameJob;

namespace RpgGame.Presenter
{
	internal class GameSystem : IDisposable, IPresenter
	{
		private readonly RpgDbContext _db = new RpgDbContext();

		// The UI interface
		private readonly IView _view;

		// Track whether Dispose has been called.
		private bool _disposed;

		public GameSystem(IView view)
		{
			_view = view;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void StartGame()
		{
			while (true)
			{
				_view.PrintMain();
				string query = _view.InputQuery();

				if (query == "1")
					if (_db.Characters.ToList().Count == 10)
						_view.PrintToMuchCharacters();
					else
						CreateCharacter();
				else if (query == "2")
					if (_db.Characters.ToList().Count == 0)
						_view.PrintNoCharacters();
					else
						CharacterFight();
				else if (query == "3")
					foreach (UserCharacter character in _db.Characters.ToList())
					{
						character.Job = _db.Jobs.ToList().Find(job => job.Id == character.JobId);
						_view.PrintInformation
						(
							character.Name, character.Job.Name, character.Level, character.Job.Attribute, character.Exp,
							character.Str, character.Job.ExtraStr == 0 ? "" : " + " + character.Job.ExtraStr,
							character.Dex, character.Job.ExtraDex == 0 ? "" : " + " + character.Job.ExtraDex,
							character.Con, character.Job.ExtraCon == 0 ? "" : " + " + character.Job.ExtraCon,
							character.TotalFightTimes, character.FailFightTimes
						);
					}
				else if (query == "4")
					if (_db.Characters.ToList().Count == 0)
						_view.PrintNoCharacters();
					else
						DeleteCharacter();
				else if (query == "5")
					CreateJob();
				else if (query == "6")
					DeleteJob();
				else if (query == "7")
					break;
				else
					_view.PrintErrorInput();
			}
		}

		public void ExitGame()
		{
			Dispose();
		}

		private void CreateCharacter()
		{
			while (true)
			{
				_view.PrintNameInput();
				string name = _view.InputQuery();
				Regex regex1 = new Regex("^[\u4e00-\u9fa5_a-zA-Z0-9]+$");
				int str = 1;
				int dex = 1;
				int con = 1;
				int point = 5;

				if (regex1.IsMatch(name))
				{
					while (point != 0)
					{
						_view.PrintCharacterInit(str, dex, con, point);
						string query = _view.InputQuery();

						if (query == "1")
						{
							str += 1;
							point -= 1;
						}
						else if (query == "2")
						{
							dex += 1;
							point -= 1;
						}
						else if (query == "3")
						{
							con += 1;
							point -= 1;
						}
						else
						{
							_view.PrintErrorInput();
						}
					}

					while (true)
					{
						_view.PrintJobAttribute();
						string query = _view.InputQuery();
						int count = 0;
						List<Job> tempJobs = new List<Job>();

						if (query == "1")
						{
							foreach (Job containerJob in _db.Jobs.ToList())
								if (containerJob is MeleeJob job)
								{
									_view.PrintJobName(count + 1, job.Name);
									tempJobs.Add(job);
									count++;
								}
						}
						else if (query == "2")
						{
							foreach (Job containerJob in _db.Jobs.ToList())
								if (containerJob is RemoteJob job)
								{
									_view.PrintJobName(count + 1, job.Name);
									tempJobs.Add(job);
									count++;
								}
						}
						else if (query == "3")
						{
							foreach (Job containerJob in _db.Jobs.ToList())
								if (containerJob is DefenseJob job)
								{
									_view.PrintJobName(count + 1, job.Name);
									tempJobs.Add(job);
									count++;
								}
						}
						else
						{
							_view.PrintErrorInput();
							continue;
						}

						if (count == 0)
						{
							_view.PrintNoThisTypeJob();
						}
						else
						{
							string choice = _view.InputQuery();
							Regex regex2 = new Regex("^[1-" + count + "].*?");
							Match match = regex2.Match(choice);

							if (match.Success)
							{
								UserCharacter character = new UserCharacter
								{
									Name = name,
									Str = str,
									Dex = dex,
									Con = con,
									JobId = tempJobs[Convert.ToInt32(match.Value) - 1].Id
								};
								character.Job = _db.Jobs.ToList().Find(job => job.Id == character.JobId);
								_db.Characters.Add(character);
								_db.SaveChanges();
								_view.PrintCreateSuccess();
								_view.PrintInformation
								(
									character.Name, character.Job.Name, character.Level, character.Job.Attribute, character.Exp,
									character.Str, character.Job.ExtraStr == 0 ? "" : " + " + character.Job.ExtraStr,
									character.Dex, character.Job.ExtraDex == 0 ? "" : " + " + character.Job.ExtraDex,
									character.Con, character.Job.ExtraCon == 0 ? "" : " + " + character.Job.ExtraCon,
									character.TotalFightTimes, character.FailFightTimes
								);
								break;
							}

							_view.PrintErrorInput();
						}
					}

					break;
				}

				_view.PrintIllegelName();
			}
		}

		private void CharacterFight()
		{
			List<UserCharacter> characters = _db.Characters.ToList();
			List<GameMonster> monsters = _db.Monsterses.ToList();
			List<Job> jobs = _db.Jobs.ToList();
			Random rnd = new Random();
			int monsterType = rnd.Next(monsters.Count);
			int monsterJob = rnd.Next(jobs.Count);
			Regex regex = new Regex("^[1-" + characters.Count + "].*?");

			for (int i = 0; i < characters.Count; i++)
				_view.PrintNameChoice(i + 1, characters[i].Name);

			while (true)
			{
				string query = _view.InputQuery();
				Match match = regex.Match(query);

				if (match.Success)
				{
					UserCharacter fighter = characters[Convert.ToInt32(match.Value) - 1];
					fighter.Job = jobs.Find(job => job.Id == fighter.JobId);
					GameMonster enemy = monsters[monsterType];
					enemy.Job = jobs[monsterJob];
					enemy.Level = fighter.Level;
					enemy.Str += fighter.Level - 1;
					enemy.Dex += fighter.Level - 1;
					enemy.Con += fighter.Level - 1;
					int characterHp = (fighter.Con + fighter.Job.ExtraCon) * 3;
					int enemyHp = (enemy.Con + enemy.Job.ExtraCon) * 3;
					int characterStr = enemy.Job.DamageBy[fighter];
					int enemyStr = fighter.Job.DamageBy[enemy];
					int attackTurn = fighter.Dex + fighter.Job.ExtraDex < enemy.Dex + enemy.Job.ExtraDex ? 1 : 0;
					_view.PrintStartFight(fighter.Name, enemy.Name, enemy.Job.Name, fighter.Dex + fighter.Job.ExtraDex,
					                      enemy.Dex + enemy.Job.ExtraDex);

					while (characterHp != 0 && enemyHp != 0)
						if (attackTurn == 1)
						{
							int preAttackHp = characterHp;
							characterHp = characterHp - enemyStr <= 0 ? 0 : characterHp - enemyStr;
							_view.PrintFightLive(enemy.Name, fighter.Name, enemyStr, fighter.Job.AntagonisticBy[enemy], preAttackHp,
							                     characterHp);
							attackTurn = 0;
						}
						else
						{
							int preAttackHp = enemyHp;
							enemyHp = enemyHp - characterStr <= 0 ? 0 : enemyHp - characterStr;
							_view.PrintFightLive(fighter.Name, enemy.Name, characterStr, enemy.Job.AntagonisticBy[fighter], preAttackHp,
							                     enemyHp);
							attackTurn = 1;
						}

					if (characterHp == 0)
					{
						_view.PrintFightResult(enemy.Name, fighter.Name);
						++fighter.TotalFightTimes;
						++fighter.FailFightTimes;
						_db.Entry(fighter).State = EntityState.Modified;
						_db.SaveChanges();
						break;
					}

					_view.PrintFightResult(fighter.Name, enemy.Name);
					++fighter.TotalFightTimes;

					if (fighter.Exp < 1000)
						fighter.Exp += 100;

					if ((fighter.Exp == 100 || fighter.Exp == 300 || fighter.Exp == 600 || fighter.Exp == 1000) &&
					    fighter.Level < 5)
					{
						_view.PrintUpgradeNotice();
						int point = 3; //新的三點
						int str = fighter.Str;
						int dex = fighter.Dex;
						int con = fighter.Con;

						while (point != 0)
						{
							_view.PrintUpgradeMenu
							(
								str, fighter.Job.ExtraStr == 0 ? "" : " + " + fighter.Job.ExtraStr,
								dex, fighter.Job.ExtraDex == 0 ? "" : " + " + fighter.Job.ExtraDex,
								con, fighter.Job.ExtraCon == 0 ? "" : " + " + fighter.Job.ExtraCon, point
							);
							string choice = _view.InputQuery();

							if (choice == "1")
							{
								str += 1;
								point -= 1;
							}
							else if (choice == "2")
							{
								dex += 1;
								point -= 1;
							}
							else if (choice == "3")
							{
								con += 1;
								point -= 1;
							}
							else
							{
								_view.PrintErrorInput();
							}
						}

						fighter.Str += str - fighter.Str;
						fighter.Dex += dex - fighter.Dex;
						fighter.Con += con - fighter.Con;
						++fighter.Level;
						_db.Entry(fighter).State = EntityState.Modified;
						_db.SaveChanges();
						_view.PrintUpgradeResult(fighter.Name, fighter.Level);
						_view.PrintInformation
						(
							fighter.Name, fighter.Job.Name, fighter.Level, fighter.Job.Attribute, fighter.Exp,
							fighter.Str, fighter.Job.ExtraStr == 0 ? "" : " + " + fighter.Job.ExtraStr,
							fighter.Dex, fighter.Job.ExtraDex == 0 ? "" : " + " + fighter.Job.ExtraDex,
							fighter.Con, fighter.Job.ExtraCon == 0 ? "" : " + " + fighter.Job.ExtraCon,
							fighter.TotalFightTimes, fighter.FailFightTimes
						);
						break;
					}

					_db.Entry(fighter).State = EntityState.Modified;
					_db.SaveChanges();
					break;
				}

				if (query == "0")
				{
					_view.PrintBackToMain();
					break;
				}

				_view.PrintErrorInput();
			}
		}

		private void DeleteCharacter()
		{
			List<UserCharacter> characters = _db.Characters.ToList();

			Regex regex = new Regex("^[1-" + characters.Count + "]+?");

			for (int i = 0; i < characters.Count; i++)
				_view.PrintNameChoice(i + 1, characters[i].Name);

			while (true)
			{
				_view.PrintDeletedNotice("角色");
				string query = _view.InputQuery();
				Match match = regex.Match(query);

				if (match.Success)
				{
					UserCharacter deleteCharacter = characters[Convert.ToInt32(match.Value) - 1];
					_view.PrintDeleteSuccess("角色", deleteCharacter.Name);
					_db.Characters.Remove(deleteCharacter);
					_db.SaveChanges();
					break;
				}

				if (query == "0")
				{
					_view.PrintBackToMain();
					break;
				}

				_view.PrintErrorInput();
			}
		}

		private void CreateJob()
		{
			Regex regex1 = new Regex("^[1-3]+?");
			Regex regex2 = new Regex("^[0-3]+?");
			Regex regex3 = new Regex("^[\u4e00-\u9fa5_a-zA-Z0-9]+$");
			string[] extras = {"STR", "DEX", "CON"};

			while (true)
			{
				_view.PrintJobAttribute();
				string query = _view.InputQuery();
				Match match1 = regex1.Match(query);

				if (match1.Success)
				{
					_view.PrintNameInput();
					string name = _view.InputQuery();
					int[] extraValues = {0, 0, 0};
					int count = 0;

					if (regex3.IsMatch(name))
					{
						while (true)
						{
							_view.PrintExtraNotice(extras[count]);
							string value = _view.InputQuery();
							Match match2 = regex2.Match(value);

							if (match2.Success)
							{
								extraValues[count] = Convert.ToInt32(match2.Value);
								count++;
							}
							else
							{
								_view.PrintErrorInput();
							}

							if (count == 3)
								break;
						}
					}
					else
					{
						_view.PrintIllegelName();
						continue;
					}

					if (extraValues.Sum() < 4)
					{
						Job[] jobs =
						{
							new MeleeJob
							{
								Attribute = "Melee"
							},
							new RemoteJob
							{
								Attribute = "Remote"
							},
							new DefenseJob
							{
								Attribute = "Defense"
							}
						};

						Job tempJob = jobs[Convert.ToInt32(query) - 1];
						tempJob.Name = name;
						tempJob.ExtraStr = extraValues[0];
						tempJob.ExtraDex = extraValues[1];
						tempJob.ExtraCon = extraValues[2];
						_db.Jobs.Add(tempJob);
						_db.SaveChanges();

						break;
					}

					_view.PrintIllegelExtraValue();
				}
				else
				{
					_view.PrintErrorInput();
				}
			}
		}

		private void DeleteJob()
		{
			List<Job> jobs = _db.Jobs.ToList();
			Regex regex = new Regex("[1-" + Convert.ToInt32(jobs.Count) + "]");

			for (int i = 0; i < jobs.Count; i++)
				_view.PrintNameChoice(i + 1, jobs[i].Name);

			while (true)
			{
				_view.PrintDeletedNotice("職業");
				string query = _view.InputQuery();
				Match match = regex.Match(query);
				bool isUsingJob = false;
				int count = 0;

				if (match.Success)
				{
					List<UserCharacter> characters = _db.Characters.ToList();
					Job deleteJob = jobs[Convert.ToInt32(match.Value) - 1];

					foreach (UserCharacter containerCharacter in characters)
					{
						containerCharacter.Job = jobs.Find(job => job.Id == containerCharacter.JobId);

						if (containerCharacter.Job.Name == deleteJob.Name)
						{
							isUsingJob = true;
							break;
						}
					}

					foreach (Job containerJob in jobs)
						if (containerJob.Attribute == deleteJob.Attribute)
							count++;

					if (isUsingJob)
					{
						_view.PrintDeleteJobDeny("職業使用中！");
						break;
					}

					if (count == 1)
					{
						_view.PrintDeleteJobDeny("無法再刪除職業！");
						break;
					}

					_view.PrintDeleteSuccess("職業", deleteJob.Name);
					_db.Jobs.Remove(deleteJob);
					_db.SaveChanges();
					break;
				}

				if (query == "0")
				{
					_view.PrintBackToMain();
					break;
				}

				_view.PrintErrorInput();
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			// If disposing equals true, dispose all managed
			// and unmanaged resources.
			if (!_disposed)
				if (disposing)
					_db.Dispose();

			// Note disposing has been done.
			_disposed = true;
		}
	}
}