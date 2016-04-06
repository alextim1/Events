using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandler
{

    public class WorkResultEventArgs : EventArgs
    {
        public int Results { get; set; }

        public WorkResultEventArgs(int result)
        {
            Results = result;
        }
    } 


    public class Counter
    {
        public event EventHandler<WorkResultEventArgs> IterationModFiveIsReached;
        public event EventHandler<WorkResultEventArgs> CalculationIsFinished;

        protected virtual void OnIterationModFiveIsReached(WorkResultEventArgs results)
        {
            var temp = IterationModFiveIsReached;
            if(temp!=null)
            {
                temp(this, results);
            }
        }


        protected virtual void OnCalculationIsFinished(WorkResultEventArgs results)
        {
            var temp = CalculationIsFinished;
            if(temp!=null)
            {
                temp(this, results);
            }
        }

        public void Calculator(int n)
        {
            int bufer = 0;
            for (int i=0;i<= n;i++)
            {
                bufer += i;

                // condition of reaching valid iteration event
                if (bufer % 5 == 0)
                    OnIterationModFiveIsReached(new WorkResultEventArgs(bufer));


             }
            // calculation is finished
            OnCalculationIsFinished(new WorkResultEventArgs(bufer));
        }
    }


    public class CounterListener
    {
        public void ValidIterationIsReachedReaction(Object sender, WorkResultEventArgs results)
        {
            Console.WriteLine(results.Results);
        }

        public void CalculationIsFinishedReaction(Object sender, WorkResultEventArgs results)
        {
            Console.WriteLine(results.Results);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Counter currentCounter = new Counter();
            CounterListener counterListener1 = new CounterListener();
            CounterListener counterListener2 = new CounterListener();

            // subscribing
            currentCounter.IterationModFiveIsReached += counterListener1.ValidIterationIsReachedReaction;
            currentCounter.CalculationIsFinished += counterListener2.CalculationIsFinishedReaction;

            Console.WriteLine("Enter max number in range to sum");
            currentCounter.Calculator(int.Parse(Console.ReadLine()));

            Console.ReadKey();


        }
    }
}
