﻿using MazeLibary.Interfaces;
using System;
using System.Collections.Generic;

namespace MazeLibary.SearchableClasses
{
    /* this class help us make the bridge pattern.
     * we want to give the class who search all the methods they need for search,
     * buy to hide other things. the search class know to run over evry maze who implement this. */
    public class SearchableMaze<T> : ISearchable<T> where T : IComparable
    {
        public IMaze<ICell<T>, T, IPosition<T>> Maze { get; set; }

        public SearchableMaze(IMaze<ICell<T>, T, IPosition<T>> m)
        {
            Maze = m;
        }

        public void SetInitialState(ICell<T> start)
        {
            Maze.Entrance = start;
        }

        public void SetGoalState(ICell<T> goal)
        {
            Maze.Exit = goal;
        }

        public List<ICell<T>> GetAllPossibleStates(ICell<T> cell)
        {
            return cell.Neighbors;
        }

        public ICell<T> GetGoalState()
        {
            return Maze.Exit;
        }

        public ICell<T> GetInitialState()
        {
            return Maze.Entrance;
        }

        public void MakeAllCellUnVisited()
        {
            Maze.UpdateNeighborsOfAllCells();
            foreach (ICell<T> i in Maze)
            {
                i.CameFrom = null;
                i.IsVisited = false;
                i.Cost = Int32.MaxValue;
            }
        }
    }
}
