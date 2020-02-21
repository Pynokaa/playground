package testwiese;

import java.util.Scanner;

public class test {

	public static void main(String[] args) {
		
		//Variable declaration
		int var1, var2; 
		double result;
		
		//Creating a new Object of the class "Scanner" with the name "in" (since Java is too stupid to have normal input readers)
		Scanner in = new Scanner(System.in);
		
		//Showing the User a text and read his input into var1
		System.out.println("Enter the first Number!");
		var1 = Integer.parseInt(in.nextLine());
		
		//Same thing
		System.out.println("Enter the second Number!");
		var2 = Integer.parseInt(in.nextLine());
		
		//Result gets the value of "var1 + var2" 
		result = var1 + var2;
		
		//Output of the Result
		System.out.println(result);
	}

}
