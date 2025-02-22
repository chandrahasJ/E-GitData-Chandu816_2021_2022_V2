﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    public class BinarySearchAlgo
    {
        public int BinarySearch(int [] arrayData, int arrayLength, int searchItem)
        {
            int left = 0;
            int right = arrayLength - 1;
         
            while (left <= right)
            {
                int middle = (left + right) / 2;
                if (searchItem == arrayData[middle])
                {
                    return middle;
                }
                else if (searchItem < arrayData[middle])
                {
                    right = middle - 1;
                }
                else if (searchItem > arrayData[middle])
                {
                    left = middle + 1;
                }
            }
            return -1;
        }

        public int BinarySearchRecursionVersion(int [] arrayData, int searchItem,int leftIndex, int rightIndex)
        {
            if (leftIndex > rightIndex)
            {
                return -1;
            }
            else
            {
                int middle = (leftIndex + rightIndex) / 2;
                if (searchItem == arrayData[middle])
                {
                    return middle;
                }
                else if (searchItem < arrayData[middle] )
                {
                    return BinarySearchRecursionVersion(arrayData, searchItem, leftIndex, middle - 1);
                }
                else if (searchItem > arrayData[middle])
                {
                    return BinarySearchRecursionVersion(arrayData , searchItem, middle +1, rightIndex);
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
