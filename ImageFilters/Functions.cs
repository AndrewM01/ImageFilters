using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    internal class Functions
    {

        public static byte ChangeValue(int median, int x, int y, byte[,] image, byte[] arr, int WindowMaxS, int Window, int arraylength, String SelectedAlgo,String SelectedFilter,int tValue)
        {

            
            int size = (Window * Window) - 1;
            byte Zxy = image[x, y];
            byte Zmax = arr[arraylength-1];
            byte Zmin = arr[0];
            byte Zmed = (byte)(median);
            byte NewPixelValue=Zmed;
            int A1 = (Zmed - Zmin);
            int A2 = (Zmax - Zmed);

            if (A1 > 0 && A2 > 0)
            {
                int B1 = (Zxy - Zmin);
                int B2 = (Zmax - Zxy);
                if (B1 > 0 && B2 > 0)
                {
                    NewPixelValue = Zxy;
                }
                else
                {
                    NewPixelValue = Zmed;
                }
            }
            else
            {
                Window = Window + 2;
                if (Window <= WindowMaxS)
                {
                    NewPixelValue = WindowSlicing1(image, x, y, Window, WindowMaxS,SelectedAlgo,SelectedFilter,tValue);
                }


                else
                {
                    NewPixelValue = Zmed;
                }

            }
            return NewPixelValue;
        }
        

        public static byte Median(byte []arr,int size)
        {
           
           
                
                return arr[size  / 2];
                
            
        }

        public static byte Mean(byte [] validArr,int arraylength)
        {
            byte MeanOfArray = 0;
            byte sum = 0;
            for(int i=0;i<arraylength;i++)
            {
                sum=(byte)(sum + validArr[i]); 
            }
            MeanOfArray = (byte)(sum/arraylength);
            return MeanOfArray ;

        }
        public static byte[] removeTvalues(byte []validArr,ref int arraylenght,int tValue)
        {
            int index = 0;
            byte[] newArr = new byte[arraylenght];
            if (tValue * 2 < arraylenght)
            {

                for (int i = 0; i < arraylenght; i++)
                {
                    if (i < tValue)
                    {
                        validArr[i] = 0;
                        if(i==0)
                        {
                            validArr[arraylenght-tValue] = 0;   
                        }
                        else
                        {
                            validArr[arraylenght - i] = 0;
                        }
                    }
                    else if(i<arraylenght-tValue)
                    {
                        newArr[index]=validArr[i];
                        index++;
                    }    
                }
                arraylenght = index;
                byte[] newArr2 = new byte[index];
                for(int j=0;j<newArr2.Length;j++)
                {
                    newArr2[j] = newArr[j];
                }
                return newArr2;
            }
            else
            {
                
                return validArr;
            }
        }
        public static byte [,] WindowSlicing(byte [,]arr,int WindowMaxS,String SelectedAlgo,String SelectedFilter,int tValue)
        {



            int windowsize = 3;

           
                for (int x = 0; x < arr.GetLength(0); x++)
                {



                    for (int y = 0; y < arr.GetLength(1); y++)
                    {


                        arr[x, y] = WindowSlicing1(arr, x, y, windowsize, WindowMaxS, SelectedAlgo,SelectedFilter,tValue);



                    }



                }
              
            
                return arr;
        }

        public static byte WindowSlicing1(byte[,]arr,int x,int y,int windowS,int WindowMaxS,String SelectedAlgo,String SelectedFilter,int tValue)
        {
            String strCount = "countingSort";
            String strQuick = "QUICK_SORT";
            String FilterAdaptive = "AdaptiveMedian";
            String FilterAlpha = "AlphaTrim";
            int[] Dx = new int[windowS * windowS];
            int[] Dy = new int[windowS * windowS];
            byte[] windowpixel = new byte[windowS * windowS];

            int index = 0;
            byte medianOfArray = 0;
            byte meanOfArray = 0;
            int arraylength = 0;

            for (int i = -(windowS / 2); i <= (windowS / 2); i++) //O(N^2)
            {
                for (int j = -(windowS / 2); j <= (windowS / 2); j++)
                {


                    Dx[index] = i;
                    Dy[index] = j;


                    index++;

                }


            }
            for (int i = 0; i < windowS * windowS; i++)
            {
                int newX = x + Dx[i];
                int newY = y + Dy[i];

                if (newX >= 0 && newX < arr.GetLength(0) && newY >= 0 && newY < arr.GetLength(1))
                {
                    windowpixel[arraylength] = arr[newX, newY];
                    arraylength++;
                }

            }
            byte[] validarr = new byte[arraylength];
            for (int t = 0; t < arraylength; t++)
            {
                validarr[t] = windowpixel[t];
            }

           if(String.Equals(SelectedAlgo,strCount)&&String.Equals(SelectedFilter,FilterAdaptive))
            {
                for (index = 0; index < arraylength; index++)
                {
                    Console.WriteLine(validarr[index]);
                }
                Console.WriteLine("Unsorted");
                validarr = Algorthims.countSort(validarr,arraylength); //O(n+k)
                for (index = 0; index < arraylength; index++)
                {
                    Console.WriteLine(validarr[index]);
                }
                Console.WriteLine("Sorted");
                medianOfArray =Median(validarr,arraylength);//O(1)
               arr[x, y] = ChangeValue(medianOfArray, x, y, arr, validarr, WindowMaxS, windowS,arraylength,SelectedAlgo,SelectedFilter,tValue);
            }
           else if(string.Equals(SelectedAlgo,strQuick) && String.Equals(SelectedFilter, FilterAdaptive))
           {
             
                validarr = Algorthims.QUICK_SORT(validarr, 0,arraylength-1); //O(nlogn)
               
                medianOfArray = Median(validarr, arraylength);//O(1)
                arr[x, y] = ChangeValue(medianOfArray, x, y, arr, validarr, WindowMaxS, windowS, arraylength, SelectedAlgo,SelectedFilter,tValue);
           }
         
            

            
            


            return arr[x, y];

        }

        public static byte[,] Filter(byte[,] Image, int WindowMaxS,String SelectedAlgo,String SelectedFilter,int tValue)
        {
            
           
                Image = Functions.WindowSlicing(Image, WindowMaxS, SelectedAlgo,SelectedFilter,tValue);
                
            
            return Image;

        }

    }

    
}
