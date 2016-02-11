//
//	Programmerare:  Timmy & Victoria
//	Datum:			2015-2016
//	Beskrivning:	Model fil till Backgammon spelet
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{

	public enum player
	{
		one,
		two 
	};

	public struct triangel  
	{
		public int antal;
		public player color;
	}


	class BackgammonModel
	{
		private Random rnd = new Random();


		// Slå tärning
		// returnar int array [4]
		public int[] letsRollTheDice()
		{
			int[] dices = new int[4];
			dices[0] = rnd.Next(1, 7);
			dices[1] = rnd.Next(1, 7);
			if (dices[0] == dices[1]) dices[3] = dices[2] = dices[1];
			return dices;
		}

		// Lägger ut startbrickor för ett nytt spel
		// returnar triangel array [26]
		public triangel[] newGame()
		{
            triangel[] spelplan = new triangel[26];

 			spelplan[0].antal = 2;
			spelplan[0].color = player.one;
			spelplan[5].antal = 5;
			spelplan[5].color = player.two;
			spelplan[6].color = player.one;
			spelplan[8].antal = 3;
			spelplan[8].color = player.two;
			spelplan[12].antal = 5;
			spelplan[12].color = player.one;
			spelplan[13].antal = 5;
			spelplan[13].color = player.two;
			spelplan[17].antal = 3;
			spelplan[17].color = player.one;
			spelplan[19].color = player.two;
			spelplan[20].antal = 5;
			spelplan[20].color = player.one;
			spelplan[25].antal = 2;
			spelplan[25].color = player.two;

           return spelplan;
		}

        //En funktion för att ta spelet till slutläge
        public triangel[] endGame()
        {
            triangel[] spelplan = new triangel[26];
            spelplan[0].antal = 7;
            spelplan[0].color = player.two;
            spelplan[1].antal = 1;
            spelplan[1].color = player.two;
            spelplan[2].color = player.two;
            spelplan[2].antal = 2;
            spelplan[3].color = player.two;
            spelplan[3].antal = 3;
            spelplan[4].color = player.two;
            spelplan[4].antal = 1;
            spelplan[5].color = player.two;
            spelplan[5].antal = 1;                
            spelplan[20].antal = 1;
            spelplan[20].color = player.one;
            spelplan[22].antal = 6;
            spelplan[22].color = player.one;
            spelplan[25].antal = 8;
            spelplan[25].color = player.one;            
            return spelplan;
        }

        //En funktion för att illustrera hur det ser ut när det är staplat många på brickor på samma triangel
        public triangel[] highStack()
        {
            triangel[] spelplan = new triangel[26];
            spelplan[16].antal = 8;
            spelplan[16].color = player.one;
            spelplan[22].antal = 3;
            spelplan[22].color = player.one;
            spelplan[23].antal = 4;
            spelplan[23].color = player.one;
            spelplan[4].antal = 12;
            spelplan[4].color = player.two;
            spelplan[3].antal = 3;
            spelplan[3].color = player.two;
            return spelplan;
        }

        public triangel[] bricksInMiddle()
        {
            triangel[] spelplan = new triangel[26];

			spelplan[6].antal = 1;
			spelplan[6].color = player.one;
            
            spelplan[19].antal = 1;
            spelplan[19].color = player.two;

            return spelplan;
        }




		//kollar om man kan flytta en bricka
		// returnar -1 om man kan gå från baren, 1 om man kan flytta bland trianglarna, 2 om man kan gå i mål, -1 om man inte kan flytta något.
        public int canMove(triangel[] spelplan, player spelare, int[] dices)
		{
			if (spelare == player.one)
				{
					if(spelplan[6].antal > 0)
					{
						foreach (int n in dices) 
							{
							if(n != 0)
								{ 
								if(spelplan[-1+n].antal <= 1 || spelplan[-1+n].color == spelare) return -1;
								}
							}
						return 0;
					}


					bool canGoal = true;
					for (int i = 1; i <= 24; i++)
					{
						int pos = correctPos(i);
						if(spelplan[pos].color == spelare && spelplan[pos].antal > 0)
						{
							if(i < 19) canGoal = false; 
							else if (canGoal && legitMoveGoal(spelplan,i,dices,spelare) != -1) return 2;


							foreach (int n in dices) 
							{
								if (n != 0)
								{ 
									if(i+n <= 24 && legitMove(spelplan,i,i+n,dices,spelare) != -1) 
									{
										if (i>18 && canGoal) return 2;
										else return 1;
									}
								}
							}

							
							
						}
					}
				}


			else
				{

					if(spelplan[19].antal > 0)
						{
							foreach (int n in dices) 
								{
								if(n != 0)
									{ 
									if(spelplan[26-n].antal <= 1 || spelplan[26-n].color == spelare) return -1;
									}
								}
							return 0;
						}

					bool canGoal = true;
					for (int i = 24; i >= 1; i--)
					{
						int pos = correctPos(i);
						if(spelplan[pos].color == spelare && spelplan[pos].antal > 0)
						{
							if(i > 6) canGoal = false; 
							else if (canGoal && legitMoveGoal(spelplan,i,dices,spelare) != -1) return 2;


							foreach (int n in dices) 
							{
								if (n != 0)
								{ 
									if(i-n >= 1 && legitMove(spelplan,i,i-n,dices,spelare) != -1) 
									{
										if (i<=6 && canGoal) return 2;
										else return 1;
									}
								}
							}

							
							
						}
					}
				}
			return 0;
		}

		//Funktion som kollar om det går att gå i mål
		//True om det går, annars false
		public bool AvailableMoveGoal(triangel[] spelplan, int first, int[] dices, player spelare)
		{
			if (legitMoveGoal(spelplan, first, dices, spelare) != -1 && canMove(spelplan, spelare, dices) == 2) return true;
			return false;
		}

		//Funktion som tar reda på alla möjliga moves för en triangel.
		public void AvailableMoves(List<int>[] moves, int listindex, triangel[] spelplan, int[] dices, player spelare, int valdtriangel)
		{
			if (valdtriangel == 25) valdtriangel = 0;
			if (valdtriangel == 26) valdtriangel = 25;
			int index = -1;
			int[] nydice = new int[4];
			triangel[] nyspelplan = new triangel[26];

			if (spelare == player.one)
			{
				for (int i = 0; i < dices.Length; i++)
				{
					if (dices[i] > 0)
					{
						if (valdtriangel == 0) index = legitMove(spelplan, -1, valdtriangel + dices[i], dices, spelare);
						else if (valdtriangel+dices[i] < 25) index = legitMove(spelplan, valdtriangel, valdtriangel + dices[i], dices, spelare);
						if (index != -1)
						{
							if (spelplan[6].antal == 0 || valdtriangel == 0)
							{
								if (!moves[listindex].Contains(valdtriangel + dices[index])) moves[listindex].Add(valdtriangel + dices[index]);
								Array.Copy(dices, nydice, 4);
								Array.Copy(spelplan, nyspelplan, 26);
								move(nyspelplan, valdtriangel, valdtriangel + dices[index], nydice, spelare);
								AvailableMoves(moves, 1, nyspelplan, nydice, spelare, valdtriangel + dices[index]);
							}
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < dices.Length; i++)
				{
					if (dices[i] > 0)
					{
						if (valdtriangel == 25) index = legitMove(spelplan, -1, valdtriangel - dices[i], dices, spelare);
						else if (valdtriangel-dices[i] > 0) index = legitMove(spelplan, valdtriangel, valdtriangel - dices[i], dices, spelare);
						if (index != -1)
						{
							if (spelplan[19].antal == 0 || valdtriangel == 25)
							{
								if (!moves[listindex].Contains(valdtriangel - dices[index])) moves[listindex].Add(valdtriangel - dices[index]);
								Array.Copy(dices, nydice, 4);
								Array.Copy(spelplan, nyspelplan, 26);
								move(nyspelplan, valdtriangel, valdtriangel - dices[index], nydice, spelare);
								AvailableMoves(moves, 1, nyspelplan, nydice, spelare, valdtriangel - dices[index]);
							}
						}
					}
				}
			}
		}



		// Flyttar en bricka.
		// returnar true om det gick, annars false.
		public bool move(triangel[] spelplan, int first, int second, int[] dices,player spelare)
		{
			if(first > 26 || second > 24 || second < 1) return false;
			if(first == 25 || first == 26 || first == 0) first = -1;
			int index = legitMove(spelplan,first,second, dices, spelare);
			if(index != -1)
			{
				if (first != -1) first = correctPos(first);
				else if (spelare == player.one) first = 6;
				else first = 19;

				second = correctPos(second);
				dices[index] = 0;

				spelplan[first].antal--;
				if(spelplan[second].antal == 1 && spelplan[second].color != spelare)
				{
					if(spelplan[second].color == player.one) spelplan[6].antal++;
					else spelplan[19].antal++;

					spelplan[second].color= spelare;
					return true;
				}

				spelplan[second].color = spelare;
				spelplan[second].antal++;
				return true;
			}

			return false;
		}

		// Tar bort bricka från spelplanen (går i mål)
		// returnar true om det gick, annars false.
		public bool moveGoal(triangel[] spelplan, int first,int[] dices,player spelare)
		{
			if(first < 1 || first > 24) return false;
			int index = legitMoveGoal(spelplan,first,dices,spelare);
			if(index != -1)
				{
				first = correctPos(first);
				dices[index] = 0;
				spelplan[first].antal--;
				return true;
				}
	
			return false;
		}



		// privat funktion som kollar om man kan gå i mål.
		// returnar int, index till tärning om det går annars -1.
		private int legitMoveGoal(triangel[] spelplan, int first, int[] dices, player spelare)
		{
			int[] dice = new int [4];
			for(int i = 0; i<4;i++) dice[i] = dices[i];
			int index = -1;
			if(spelare==player.one)
				{
				for(int i=20; i<=25; i++)
					{
					if(spelplan[i].antal == 0 || spelplan[i].color == player.two)
						{
						for(int j = 0; j<4;j++)
							{
								if(dice[j]==26-i) dice[j]--;
							}
						}
					else break;
					}
				for(int i=0;i<dice.Length;i++) if (dice[i]+first == 25) index = i;
				}

			if(spelare==player.two)
				{
				for(int i=5; i>=0; i--)
					{
					if(spelplan[i].antal == 0 || spelplan[i].color == player.one)
						{
						for(int j = 0; j<4;j++)
							{
								if(dice[j]==1+i) dice[j]--;
							}
						}
					else break;
					}
				for(int i=0;i<dice.Length;i++) if (first-dice[i] == 0) index = i;
				}


			

			return index;
		}


		// privat funktion som kollar om man kan flytta brickan.
		// returnar int, index till tärning om det går annars -1.
		private int legitMove(triangel[] spelplan, int first, int second, int[] dices, player spelare)
		{
			int langd;
			int indextarning = -1;

			if(spelare==player.two) 
			{
				if(first == -1) langd = 25-second;
				else
					{
						if(second >= first) return -1;
						langd = first-second;
					}
			}

			else 
			{
				if(first == -1) langd = second;
				else
					{ 
						if(first >= second) return -1;
						langd = second-first;
					}
			}



				for(int i=0;i<dices.Length;i++) if (dices[i]==langd) indextarning = i;
				if(indextarning == -1) return -1;

				if (first != -1) first = correctPos(first);
				else if(spelare == player.one) first = 6;
				else first = 19;
				second = correctPos(second);

				if (spelplan[first].color != spelare || spelplan[first].antal == 0) return -1;
				if (spelplan[second].antal <= 1 || spelplan[second].color == spelare) 
				{
					return indextarning;
				}
				



			return -1;

		}



		// Funktion som rättar vald plats till elementets plats i arrayen.
		// returnar int, arrayets index för inskickad triangel position.
		public int correctPos(int spelplanPos)
		{
			if (spelplanPos > 0 && spelplanPos <= 6) return spelplanPos-1;
			if (spelplanPos > 6 && spelplanPos <= 18) return spelplanPos;
			if (spelplanPos > 18 && spelplanPos <= 24) return spelplanPos+1;
			if(spelplanPos == 25) return 6;
			if(spelplanPos == 26) return 19;
			else return 19;
		}


		//SelfTest
		public static bool SelfTest()
		{
			bool ok = true;
			BackgammonModel test = new BackgammonModel();

			// Test för Triangel
			
			triangel [] test1 = new triangel [4];
			test1[0].antal = 3;
			test1[0].color = player.two;
			test1[1].antal = 1;
			test1[1].color = player.one;

			ok = ok && test1[0].antal == 3 && test1[0].color == player.two;
			ok = ok && test1[1].antal == 1 && test1[1].color == player.one;
			ok = ok && test1[2].antal == 0 && test1[2].color == player.one;
			ok = ok && test1[3].antal == 0 && test1[3].color == player.one;

			ok = ok && (int)player.two == 1;
			ok = ok && (int)player.one == 0;
			
			System.Diagnostics.Debug.WriteLine("Triangel " + ok);

			// Test för LetsRollTheDice()
			for (int i = 0; i < 100 && ok; i++)
			{
				int[] dices = test.letsRollTheDice();
				ok = ok && dices[0] >= 1 && dices[0] <= 6 && dices[1] >= 1 && dices[1] <= 6;
				if (dices[0] == dices[1]) if (dices[1] != dices[2] && dices[2] != dices[3]) ok = false;
			}
			System.Diagnostics.Debug.WriteLine("LetsRollTheDice " + ok);

			// Test för newGame()
			triangel[] testspelplan = new triangel[5];

			for(int i = 0; i<5;i++) testspelplan[i].antal = 1;

			testspelplan = test.newGame();

			for(int i = 0; i<5;i++) ok = ok && testspelplan[i].antal != 1;
			ok = ok && testspelplan.Length==26;
			System.Diagnostics.Debug.WriteLine("newGame " + ok);

			// Test för correctPos()
						ok = ok && test.correctPos(1) == 0;
			ok = ok && test.correctPos(6) == 5;
			ok = ok && test.correctPos(7) == 7;
			ok = ok && test.correctPos(18) == 18;
			ok = ok && test.correctPos(19) == 20;
			ok = ok && test.correctPos(24) == 25;
			System.Diagnostics.Debug.WriteLine("correctPos " + ok);
			
            //Test för legitMove()
            int[] dices1 = { 2, 1, 0, 0 };
			testspelplan = test.newGame();

            // Spelare Black
            ok = ok && test.legitMove(testspelplan, 13, 11, dices1, player.two) == 0; 
            ok = ok && test.legitMove(testspelplan, 20, 19, dices1, player.two) == -1; 
            ok = ok && test.legitMove(testspelplan, 21, 19, dices1, player.two) == -1; 
            ok = ok && test.legitMove(testspelplan, 19, 21, dices1, player.two) == -1; 
            ok = ok && test.legitMove(testspelplan, 13, 12, dices1, player.two) == -1; 
			dices1[0] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == 0;
 			dices1[1] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == 1; 
			dices1[2] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == 2; 
			dices1[3] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == 3;
			dices1[0] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == 3;
			dices1[1] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == 3; 
			dices1[2] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == 3; 
			dices1[3] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, player.two) == -1;
			//spela från bar
			testspelplan[19].antal = 1;

			for(int i = 0; i<4;i++) dices1[i] = i+1;

			ok = ok && test.legitMove(testspelplan, -1, 1, dices1, player.two) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, player.two) == 0;
			ok = ok && test.legitMove(testspelplan, -1, 23, dices1, player.two) == 1;
			ok = ok && test.legitMove(testspelplan, -1, 22, dices1, player.two) == 2;
			ok = ok && test.legitMove(testspelplan, -1, 21, dices1, player.two) == 3;
			testspelplan[19].antal = 0;
			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, player.two) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 23, dices1, player.two) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 22, dices1, player.two) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 21, dices1, player.two) == -1;
			testspelplan[19].antal = 1;

			for(int i = 25; i>21;i--) 
			{
				testspelplan[i].antal = 2;
				testspelplan[i].color = player.one;
				ok = ok && test.legitMove(testspelplan, -1, i-1, dices1, player.two) == -1;
			}

	


			//Spelare White
			dices1[0] = 2;
			dices1[1] = 1;
			dices1[2] = 0;
			dices1[3] = 0;
            ok = ok && test.legitMove(testspelplan, 1, 3, dices1, player.one) == 0;  
            ok = ok && test.legitMove(testspelplan, 8, 10, dices1, player.one) == -1; 
            ok = ok && test.legitMove(testspelplan, 22, 20, dices1, player.one) == -1;
            ok = ok && test.legitMove(testspelplan, 12, 10, dices1, player.one) == -1;
            ok = ok && test.legitMove(testspelplan, 17, 20, dices1, player.one) == -1;
			dices1[0] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == 0; 
			dices1[1] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == 1;
			dices1[2] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == 2;
			dices1[3] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == 3;
			dices1[0] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == 3; 
			dices1[1] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == 3;
			dices1[2] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == 3;
			dices1[3] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, player.one) == -1;


            //spela från bar
			testspelplan[6].antal = 1;

			for(int i = 0; i<4;i++) dices1[i] = i+1;

			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, player.one) == -1;

			for (int i = 0; i < 4; i++)
			{
				ok = ok && test.legitMove(testspelplan, -1, i + 1, dices1, player.one) == i;
			}

			testspelplan[6].antal = 0;

			for (int i = 0; i < 4; i++)
			{
				ok = ok && test.legitMove(testspelplan, -1, i + 1, dices1, player.one) == -1;
			}

			testspelplan[6].antal = 1;

			for (int i = 0; i < 4; i++)
			{
				testspelplan[i].antal = 2;
				testspelplan[i].color = player.two;
				ok = ok && test.legitMove(testspelplan, -1, i + 1, dices1, player.one) == -1;
			}


            System.Diagnostics.Debug.WriteLine("legitMove " + ok);

            //Test för legitMoveGoal()
						testspelplan = new triangel[26];
			testspelplan[25].antal = 1;
			testspelplan[25].color = player.one;
			testspelplan[0].antal = 1;
			testspelplan[0].color = player.two;
			for(int i = 0; i<4;i++) dices1[i] = 6;
			ok = ok && test.legitMoveGoal(testspelplan,24,dices1,player.one) == 3;
			ok = ok && test.legitMoveGoal(testspelplan,1,dices1,player.two) == 3;
			ok = ok && test.legitMoveGoal(testspelplan,24,dices1,player.one) == 3;
			ok = ok && test.legitMoveGoal(testspelplan,1,dices1,player.two) == 3;

            System.Diagnostics.Debug.WriteLine("legitMoveGoal " + ok);

            //Test för moveGoal()
			testspelplan = new triangel[26];
			testspelplan[25].antal = 1;
			testspelplan[25].color = player.one;
			testspelplan[0].antal = 1;
			testspelplan[0].color = player.two;
			for(int i = 0; i<4;i++) dices1[i] = 6;
			ok = ok && test.moveGoal(testspelplan,24,dices1,player.one) == true;
			ok = ok && test.moveGoal(testspelplan,1,dices1,player.two) == true;
			ok = ok && test.moveGoal(testspelplan,24,dices1,player.one) == false;
			ok = ok && test.moveGoal(testspelplan,1,dices1,player.two) == false;
            System.Diagnostics.Debug.WriteLine("moveGoal " + ok);

            //Test för move()
			testspelplan = test.newGame();

            //Spelare Black
            dices1[0] = 2;
            dices1[1] = 1;
            dices1[2] = 0;
            dices1[3] = 0;
            ok = ok && test.move(testspelplan, 13, 11, dices1, player.two) == true;
            ok = ok && test.move(testspelplan, 20, 19, dices1, player.two) == false;
            ok = ok && test.move(testspelplan, 21, 19, dices1, player.two) == false;
            ok = ok && test.move(testspelplan, 19, 21, dices1, player.two) == false;
            ok = ok && test.move(testspelplan, 13, 12, dices1, player.two) == false;

            dices1[0] = 4;
            ok = ok && test.move(testspelplan, 24, 20, dices1, player.two) == true;
            dices1[1] = 4;
            ok = ok && test.move(testspelplan, 24, 20, dices1, player.two) == true;
         
            //spela från bar
            testspelplan[19].antal = 1;
            ok = ok && test.move(testspelplan, -1, 1, dices1, player.two) == false;


            //Spelare White
            dices1[0] = 2;
            dices1[1] = 1;
            dices1[2] = 0;
            dices1[3] = 0;
            ok = ok && test.move(testspelplan, 1, 3, dices1, player.one) == true;
            ok = ok && test.move(testspelplan, 8, 10, dices1, player.one) == false;
            ok = ok && test.move(testspelplan, 22, 20, dices1, player.one) == false;
            ok = ok && test.move(testspelplan, 12, 10, dices1, player.one) == false;
            ok = ok && test.move(testspelplan, 17, 20, dices1, player.one) == false;
            dices1[0] = 4;
            ok = ok && test.move(testspelplan, 1, 5, dices1, player.one) == true;
            dices1[1] = 4;
   
            //spela från bar
            testspelplan[6].antal = 1;
            ok = ok && test.move(testspelplan, -1, 21, dices1, player.one) == false;
            System.Diagnostics.Debug.WriteLine("move " + ok);

            //Test för canMove()
            testspelplan = test.newGame();

            //spelare Black
            ok = ok && test.canMove(testspelplan, player.two, dices1) == 1;
            testspelplan[19].antal = 1;
            for (int i = 0; i < 4; i++) dices1[i] = i + 1;
            ok = ok && test.canMove(testspelplan, player.two, dices1) == -1;
        

            //spelare White
            ok = ok && test.canMove(testspelplan, player.one, dices1) == 1;
            testspelplan[6].antal = 1;
            for (int i = 0; i < 4; i++) dices1[i] = i + 1;
            ok = ok && test.canMove(testspelplan, player.two, dices1) == -1;
            System.Diagnostics.Debug.WriteLine("canMove " + ok);

			//Test AvailableMoveGoal()
			//
            testspelplan = new triangel[26];
            testspelplan[2].antal = 1;
            testspelplan[2].color = player.two;
            testspelplan[3].antal = 2;
            testspelplan[3].color = player.two;
            testspelplan[4].antal = 2;
            testspelplan[4].color = player.two;
            testspelplan[5].antal = 2;
            testspelplan[5].color = player.two;
            testspelplan[6].antal = 5;
            testspelplan[6].color = player.one;
            testspelplan[8].antal = 3;
            testspelplan[8].color = player.two;
            testspelplan[12].antal = 5;
            testspelplan[12].color = player.one;
            dices1[0] = 2;
            dices1[1] = 1;

            triangel[] testspelplan1 = new triangel[26];
			testspelplan1[0].antal = 5;
            testspelplan1[0].color = player.two;
            testspelplan1[1].antal = 5;
            testspelplan1[1].color = player.two;
            testspelplan1[2].antal = 4;
            testspelplan1[2].color = player.two;
			testspelplan1[3].antal = 1;
			testspelplan1[3].color = player.two;
            testspelplan1[22].antal = 2;
			testspelplan1[22].color = player.one;
            testspelplan1[23].antal = 5;
			testspelplan1[23].color = player.one;
            testspelplan1[24].antal = 3;
			testspelplan1[24].color = player.one;
            testspelplan1[25].antal = 5;
			testspelplan1[25].color = player.one;

            //Spelare One
            ok = ok && test.AvailableMoveGoal(testspelplan, 3, dices1, player.one) == false;
            ok = ok && test.AvailableMoveGoal(testspelplan, 8, dices1, player.one) == false;
           
			ok = ok && test.AvailableMoveGoal(testspelplan1, 25, dices1, player.one) == false;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 24, dices1, player.one) == true;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 23, dices1, player.one) == true;
            dices1[0] = 4;
            dices1[1] = 4;
            dices1[2] = 4;
            dices1[3] = 4;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 25, dices1, player.one) == false;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 24, dices1, player.one) == false;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 23, dices1, player.one) == false;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 22, dices1, player.one) == false;
			testspelplan1[21].antal = 0;
            testspelplan1[22].antal = 0;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 22, dices1, player.one) == true;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 23, dices1, player.one) == false;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 24, dices1, player.one) == false;
            ok = ok && test.AvailableMoveGoal(testspelplan1, 25, dices1, player.one) == false;
			testspelplan1[23].antal = 0;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 22, dices1, player.one) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 23, dices1, player.one) == true;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 24, dices1, player.one) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 25, dices1, player.one) == false;
            testspelplan1[24].antal = 0;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 22, dices1, player.one) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 23, dices1, player.one) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 24, dices1, player.one) == true;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 25, dices1, player.one) == false;
			testspelplan1[25].antal = 0;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 22, dices1, player.one) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 23, dices1, player.one) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 24, dices1, player.one) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 25, dices1, player.one) == false;

            //Spelare Two
			dices1[0] = 2;
			dices1[1] = 1;
			ok = ok && test.AvailableMoveGoal(testspelplan, 12, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan, 6, dices1, player.two) == false;

			ok = ok && test.AvailableMoveGoal(testspelplan1, 0, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 1, dices1, player.two) == true;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 2, dices1, player.two) == true;
			dices1[0] = 4;
			dices1[1] = 4;
			dices1[2] = 4;
			dices1[3] = 4;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 0, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 1, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 2, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 3, dices1, player.two) == false;
			testspelplan1[4].antal = 0;
			testspelplan1[3].antal = 0;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 3, dices1, player.two) == true;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 2, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 1, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 0, dices1, player.two) == false;
			testspelplan1[2].antal = 0;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 3, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 2, dices1, player.two) == true;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 1, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 0, dices1, player.two) == false;
			testspelplan1[1].antal = 0;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 3, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 2, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 1, dices1, player.two) == true;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 0, dices1, player.two) == false;
			testspelplan1[0].antal = 0;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 3, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 2, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 1, dices1, player.two) == false;
			ok = ok && test.AvailableMoveGoal(testspelplan1, 0, dices1, player.two) == false;

			System.Diagnostics.Debug.WriteLine("AvailableMoveGoal " + ok);

            return ok;
		}


	}
}