using System;

namespace LINQSamples
{
  class Program
  {
    static void Main(string[] args)
    {
      // Instantiate the ViewModel
      SamplesViewModel vm = new SamplesViewModel
      {
        // Use Query or Method Syntax?
        UseQuerySyntax = true
      };

            // Call a sample method
            //vm.ForEach();
            //vm.Take();
            //vm.TakeWhile();
            //vm.Skip();
            //vm.SkipWhile();
            //vm.Distinct();
            //vm.All();
            //vm.Any();
            //vm.LINQContains();
            //vm.LINQContainsUsingComparer();
            //vm.SequenceEqualIntegers();
            //vm.SequenceEqualProducts();
            //vm.SequenceEqualUsingComparer();
            //vm.ExceptIntegers();
            //vm.Except();
            //vm.Intersect();
            //vm.Concat();
            //vm.InnerJoinTwoFields();
            //vm.GroupBy();
            vm.GroupByOrderByKey();
      // Display Product Collection
      foreach (var item in vm.Products) {
        Console.Write(item.ToString());
      }

      // Display Result Text
      Console.WriteLine(vm.ResultText);
    }
  }
}
