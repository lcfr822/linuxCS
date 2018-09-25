using System;

public static class InterestCalculator{
	//args[0] = start balance
	//args[1] = interest %
	//args[1] = compounds per year
	//args[2] = years
	public static void Main(string[] args){
		float balance = float.Parse(args[0]);
		float interest = float.Parse(args[1]);
		int compoundsPerYear = int.Parse(args[2]);
		
		for(int i = 0; i < int.Parse(args[3]); i++){
			for(int o = 0; o < compoundsPerYear; o++){
				balance += balance * interest / compoundsPerYear;
			}
		}
		
		Console.WriteLine("$" + balance.ToString("#,###.##"));
	}
}
