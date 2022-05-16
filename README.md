# Sudoku-solver
A WPF application that solves a sudoku that you enter.

The UI is ugly because it was not the point of this project I was more interested in creating the solver. 


The "interesting" stuff is in the solveMachine class. The way it solves te puzzle is that it first looks for squares that have least possible values to put.
Then if there is only one value to put (that means it's 100% sure it's correct) it inputs the value and keeps going. When it encounters a multiple possible values
it creates new "branches" where it inputs all the possibilities to that square. Then it chooses the first value and keeps going until it solves the puzzle or
if it runs to a deadend it returns to the branching point and chooses the next value. 

Because it always tries to get to the end of a branch first before checking other branches you can input a empty board to it and 
it will not go through millions of possibilities.

Usually this solves the sudokus in 100-500 branches but sometimes if you only enter 1-3 values in random places it can take thousands of branches.
the most I got was over 6000.

This is not a sudoku game it's only a solver.

I also tried to do a little TDD but because I was figuring out what the program was while I was making it the tests and actual code were developed hand in hand.

It could also be used to create sudoku puzzles: Tell it to input random numbers to random places and look if it's possible to solve. 
Then just return the values and the squares they were in. Difficulty could be chosen by setting the amount of numbers it inputs.
