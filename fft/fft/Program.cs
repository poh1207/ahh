using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace fft
{
    

    //lena
    struct COMPLEX
    {
        public double real, imag;
        public COMPLEX(double x, double y)
        {
            real = x;
            imag = y;
        }
        public float Magnitude()
        {
            return (  (float) Math.Sqrt(real * real + imag * imag)  );
        }
        public float Phase()
        {
            return ((float)Math.Atan(imag / real));
        }
    }
   

    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("C:\\Users\\poh12\\OneDrive\\Desktop\\lena_256.raw", FileMode.Open, FileAccess.Read);

            //FileStream fs = new FileStream("C:\\Users\\poh12\\OneDrive\\Desktop\\MR_data.raw", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            //Stream s = new MemoryStream();


            int pixelSize = 256;

            COMPLEX[,] Fourier = new COMPLEX[pixelSize, pixelSize];



            // byte[] buffer= new byte[4];
            int[,] input = new int[256, 256];

            for (int j = 0; j < pixelSize; j++)
            {
                for (int k = 0; k < pixelSize; k++)
                {

                    //lena
                    input[j, k] = (int)br.ReadByte();
                    //Fourier[j, k].real =(double) br.ReadByte();//lena                                      
                    //Fourier[j, k].imag = 0;//(double)br.ReadByte();
                    //  Console.WriteLine(Fourier[j, k].real + " " + Fourier[j, k].imag);
                }
            }

            br.Close();
            fs.Close();

            
           
            
             
            // mri
            FileStream fs0 = new FileStream("D:/lena_fft7.raw", FileMode.Create, FileAccess.Write);
            BinaryWriter bw0 = new BinaryWriter(fs0);

            

             FFT nana = new FFT(input);

            nana.ForwardFFT();
            nana.InverseFFT();
            
            
            


            //lena
            for (int j = 0; j < pixelSize; j++)
            {
                for (int k = 0; k < pixelSize ; k++)
                {

                    bw0.Write((byte) nana.Fourier[j,k].Magnitude() );
                    //bw0.Write(  (byte)Fourier[j, k].Magnitude()  );//lena
                                                               //r[j, k].real + " " + Fourier[j, k].imag + " " + Fourier[j, k].Magnitude());
                }
            }


            bw0.Close();
            fs0.Close();
            


            
        }






















    }
}
