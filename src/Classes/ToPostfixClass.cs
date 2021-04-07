using System;
using System.Collections.Generic;
using System.Windows; //TODO only for disgnostics, remove later
using System.Diagnostics; //TODO only for disgnostics, remove later

namespace Calculator.Classes
{
	public class ToPostfixClass
	{
		private string _output { get; set; }

		public string GetResult()
		{
			return _output;
		}

		static public List<Token> toPostfix(List<Token> tokens)
		{
			// Vytvořen nový list pro výsledky
			List<Token> result = new List<Token>();

			// Iniciace stacku pro tokeny
			Stack<Token> stack = new Stack<Token>();

			foreach (Token token in tokens)
			{ 
				// Pokud je token operand, přidá jej do výsledku
				if (token.type == TokenType.operand)
				{
					result.Add(token);
				}
				else if (token.type == TokenType.brackets)
				{// Pokud je token otevírací závorka, přidá ji na stack 
					if (token.operation == '(')
                    {
						stack.Push(token);
					}
					else if (token.operation == ')')
					{// Pokud je token  uzavírací závorka, tahá ze stacku tokeny dokud nenarazí na otevírací závorku.
						while (stack.Count > 0 && stack.Peek().operation != '(')
						{
							result.Add(stack.Pop());
						}

						if (stack.Count > 0 && stack.Peek().operation == '(')
						{
							stack.Pop();
						}
                        else
                        {
							MessageBox.Show("I pooped myself in toPostfix"); // TODO, nějakej pěknej chybovej message
						}
					}
				}
				else
				{// Token je operátor, je postupováno podle pravidel převodu do postfix při operátoru na vstupu.
					while (stack.Count > 0 && token.type <= stack.Peek().type)
					{
						result.Add(stack.Pop());
					}
					stack.Push(token);
				}
			}

			while (stack.Count > 0)
			{ // Pokud je dosažen konec výrazu, jsou ze stacku vytaženy všechny zbylé tokeny
				result.Add(stack.Pop());
			}

			return result;
		}
	}
}