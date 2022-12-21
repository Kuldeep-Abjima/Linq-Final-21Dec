using LINQSamples.EntityClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;

namespace LINQSamples
{
    public class SamplesViewModel
    {
        #region Constructor
        public SamplesViewModel()
        {
            // Load all Product Data
            Products = ProductRepository.GetAll();
            // Load all Sales Data
            Sales = SalesOrderDetailRepository.GetAll();
        }
        #endregion


        public bool UseQuerySyntax { get; set; } = true;
        public List<Product> Products { get; set; }
        public List<SalesOrderDetail> Sales { get; set; }
        public string ResultText { get; set; }

        public void ForEach()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax

            }
            else
            {
                // Method Syntax

            }

            ResultText = $"Total Products: {Products.Count}";
        }

        public void ForEachCallingMethod()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            let tmp = prod.NameLength =
                            prod.Name.Length
                            select prod).ToList();
            }
            else
            {
                // Method Syntax
                Products.ForEach(prod => prod.NameLength = prod.Name.Length);

            }

            ResultText = $"Total Products: {Products.Count}";
        }

        private decimal SalesForProduct(Product prod)
        {
            return Sales.Where(sale => sale.ProductID == prod.ProductID)
                        .Sum(sale => sale.LineTotal);
        }
        public void Take()
        {
            if (UseQuerySyntax)
            {
                // Query Syntax
                Products = (from prod in Products
                            orderby prod.Name
                            select prod).Take(5).ToList();

            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).Take(5).ToList();

            }

            ResultText = $"Total Products: {Products.Count}";
        }

        public void TakeWhile()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                            orderby prod.Name
                            select prod).TakeWhile(prod => prod.Name.
                            StartsWith("A")).ToList();

            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).
                            TakeWhile(prod => prod.Name.StartsWith("A")).ToList();

            }

            ResultText = $"Total Products: {Products.Count}";
        }

        public void Skip()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                            orderby prod.Name
                            select prod).Skip(20).ToList();
            }
            else
            {
                Products = Products.OrderBy(prod => prod.Name).
                          Skip(20).ToList();

            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void SkipWhile()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                            orderby prod.Name
                            select prod).
                            SkipWhile(prod => prod.Name.StartsWith("A")).
                            ToList();

            }
            else
            {
                // Method Syntax
                Products = Products.OrderBy(prod => prod.Name).
                            SkipWhile(prod => prod.Name.StartsWith("A")).ToList();

            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void Distinct()
        {
            List<string> colors = new List<string>();

            if (UseQuerySyntax)
            {
                colors = (from prod in Products select prod.Color).
                            Distinct().ToList();

            }
            else
            {
                // Method Syntax
                colors = Products.Select(prod => prod.Color).
                            Distinct().ToList();

            }

            // Build string of Distinct Colors
            foreach (var color in colors)
            {
                Console.WriteLine($"Color: {color}");
            }
            Console.WriteLine($"Total Colors: {colors.Count}");

            // Clear products
            Products.Clear();
        }
        public void All()
        {
            string search = " ";
            bool value;
            if(UseQuerySyntax )
            {
                value = (from prod in Products select prod).
                    All(prod => prod.Name.Contains(search));
            }
            else
            {
               value = Products.All(prod => prod.Name.Contains(search));    
            }
            ResultText = $"Do all Name properties contain a '{search}'? '{value}'";
        }
        public void Any()
        {
            string search = "z";
            bool value;
            if(UseQuerySyntax)
            {
                value = (from prod in Products select prod).Any(prod => prod.
                Name.Contains(search));
            }
            else
            {
                value = Products.Any(prod => prod.Name.Contains(search));
            }
            ResultText = $"Do any Name properties conations ac '{search}'?{value}";
        }
        public void LINQContains()
        {
            bool value;
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            if(UseQuerySyntax )
            {
                value = (from num in numbers select num).Contains(3);
            }
            else
            {
                value = numbers.Contains(3);
            }
            ResultText = $"Is the number in collection?{value}";
            Products.Clear();
        }
        public void LINQContainsUsingComparer()
        {
            int search = 744;
            bool value;
            ProductIdComparer pc = new ProductIdComparer();
            Product prodToFind = new Product { ProductID = search };
            if(UseQuerySyntax)
            {
                value = (from prod in Products select prod).Contains(prodToFind, pc);
            }
            else
            {
                value = Products.Contains(prodToFind, pc);
            }
            ResultText = $"Prodcuts ID: {search} is in Products Collection = {value}";
        }
        public void SequenceEqualIntegers()
        {
            bool value;
            List<int> list1 = new List<int> { 5,2,3,4,5};
            List<int> list2 = new List<int> {1,2,3,4,5};
            if(UseQuerySyntax )
            {
                value = (from num in list1 select num).SequenceEqual(list2);
            }
            else
            {
                value = list1.SequenceEqual(list2);
            }
            if (value)
            {
                ResultText = "List are equale";
            }
            else
            {
                ResultText = "lists are not equal";
            }
            
        }
        public void SequenceEqualProducts()
        {
            bool value;
            List<Product> l1 = new List<Product>
        {
            new Product{ProductID = 1, Name ="Product 1"},
            new Product{ProductID = 2, Name = "Products 2"}
        };
            List<Product> l2 = new List<Product> {
            new Product { ProductID = 1, Name = "Product 1"},
            new Product{ProductID = 2,Name = "Product 2"}
        };
            if (UseQuerySyntax)
            {
                value = (from prod in l1 select prod).SequenceEqual(l2);
            }
            else
            {
                value = l1.SequenceEqual(l2);
            }
            if (value)
            {
                ResultText = "Lists are Equal";
            }
            else
            {
                ResultText = "Lists are NOT Equal";
            }
        }
        public void SequenceEqualUsingComparer()
        {
            bool value;
            ProductComparer pc = new ProductComparer(); 
            List<Product> l1 = ProductRepository.GetAll();
            List<Product> l2 = ProductRepository.GetAll();

            //l1.RemoveAt(0);

            if(UseQuerySyntax)
            {
                value = (from prod in l1 select prod).SequenceEqual(l2, pc);
            }
            else
            {
                value = l1.SequenceEqual(l2, pc);
            }
            if (value)
            {
                ResultText = "yes equal";
            }
            else
            {
                ResultText = "not equal";
            }
            Products.Clear();

        }
        public void ExceptIntegers()
        {
            List<int> exceptions;
            List<int> list1 = new List<int> { 1, 2, 3, 4};
            List<int> list2 = new List<int> { 3,4,5,};
            if (UseQuerySyntax)
            {
                exceptions = (from num in list1 select num).Except(list2).ToList();
            }
            else
            {
                exceptions = list1.Except(list2).ToList();
            }
            ResultText = string.Empty;
            foreach(var item in exceptions)
            {
                ResultText += "Number; " + item + Environment.NewLine;
            }
            Products.Clear();
        }
        public void Except()
        {
            ProductComparer pc = new ProductComparer();
            List<Product> l1 = ProductRepository.GetAll();
            List<Product> l2 = ProductRepository.GetAll();

            l2.RemoveAll(prd => prd.Color == "Black");

            if(UseQuerySyntax)
            {
                Products = (from prd in l1 select prd).Except(l2, pc).ToList();
            }
            else
            {
                Products = l1.Except(l2).ToList();
            }
            ResultText = $"Total Products : {Products.Count}";
        }
        public void Intersect()
        {
            ProductComparer pc = new ProductComparer();

            List<Product> l1 = ProductRepository.GetAll();
            List<Product> l2 = ProductRepository.GetAll();

            l1.RemoveAll(prod => prod.Color == "Black");
            l2.RemoveAll(prod => prod.Color == "Red");
            if(UseQuerySyntax)
            {
                Products = (from prod in l1 select prod).Intersect(l2, pc).ToList();
            }
            else
            {
                Products = l1.Intersect(l2, pc).ToList();
            }
            ResultText = $"Total Product: {Products.Count}"; 
        }
        public void Union()
        {
            ProductComparer pc = new ProductComparer();
            List<Product> list1 = ProductRepository.GetAll();

            List<Product> list2 = ProductRepository.GetAll();

            if(UseQuerySyntax)
            {
                Products = (from prod in list1 select prod).Union(list2, pc).
                    OrderBy(prod => prod.Name).ToList();
            }
            else
            {
                Products = list1.Union(list2, pc).OrderBy(prod => prod.Name).
                    ToList();
            }
            ResultText = $"total {Products.Count}";
        }
        public void Concat()
        {
            ProductComparer pc = new ProductComparer();
            List<Product> list1 = ProductRepository.GetAll();

            List<Product> list2 = ProductRepository.GetAll();

            if (UseQuerySyntax)
            {
                Products = (from prod in list1 select prod).Concat(list2).
                    OrderBy(prod => prod.Name).ToList();
            }
            else
            {
                Products = list1.Concat(list2).OrderBy(prod => prod.Name).
                    ToList();
            }
            ResultText = $"total {Products.Count}";
        }
        public void InnerJoin()
        {
           StringBuilder sb = new StringBuilder(2048);
            int count = 0;
            if(UseQuerySyntax)
            {
                var query = (from prod in Products
                             join sale in Sales
                             on prod.ProductID equals sale.ProductID
                             select new
                             {
                                 prod.ProductID,
                                 prod.Name,
                                 prod.Color,
                                 prod.StandardCost,
                                 prod.ListPrice,
                                 prod.Size,
                                 sale.SalesOrderID,
                                 sale.OrderQty,
                                 sale.UnitPrice,
                                 sale.LineTotal
                             });
                foreach(var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order: {item.SalesOrderID}");
                    sb.AppendLine($"Product ID: {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size : {item.Size}");
                    sb.AppendLine($"Total : {item.LineTotal}");
                }
                ResultText = $"total sales {Products.Count}";
                             
            }
            else
            {
                var query = Products.Join(Sales, prod => prod.ProductID,
                    sale => sale.ProductID, (prod, sale) => new
                    {
                        prod.ProductID,
                        prod.Name,
                        prod.Color,
                        prod.StandardCost,
                        prod.ListPrice,
                        prod.Size,
                        sale.SalesOrderID,
                        sale.OrderQty,
                        sale.UnitPrice,
                        sale.LineTotal
                    });
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order: {item.SalesOrderID}");
                    sb.AppendLine($"Product ID: {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size : {item.Size}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");
                }
                
            }
            ResultText = sb.ToString() + Environment.NewLine + "Total sales: " + count.ToString();
        }
        public void InnerJoinTwoFields()
        {
            short qty = 6;
            int count = 0;

            StringBuilder sb = new StringBuilder(2048);

            if (UseQuerySyntax)
            {
                var query = (from prod in Products
                             join sale in Sales on new
                             {
                                 prod.ProductID,
                                 Qty = qty
                             }
                equals
                new { sale.ProductID, Qty = sale.OrderQty }
                             select new
                             {
                                 prod.ProductID,
                                 prod.Name,
                                 prod.Color,
                                 prod.StandardCost,
                                 prod.ListPrice,
                                 prod.Size,
                                 sale.SalesOrderID,
                                 sale.OrderQty,
                                 sale.UnitPrice,
                                 sale.LineTotal
                             });
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order: {item.SalesOrderID}");
                    sb.AppendLine($"Product ID: {item.ProductID}");
                    sb.AppendLine($"Product Name : {item.Name}");
                    sb.AppendLine($"Size : {item.Size}");
                    sb.AppendLine($"Total : {item.LineTotal:c}");
                }
            }
            else
            {
                var query = Products.Join(Sales,
                    prod => new { prod.ProductID, Qty = qty },
                    sale => new { sale.ProductID, Qty = sale.OrderQty },
                    (prod, sale) => new
                    {
                        prod.ProductID,
                        prod.Name,
                        prod.Color,
                        prod.StandardCost,
                        prod.ListPrice,
                        prod.Size,
                        sale.SalesOrderID,
                        sale.OrderQty,
                        sale.UnitPrice,
                        sale.LineTotal
                    });
            }
            ResultText = sb.ToString() + Environment.NewLine + "Total sales: " + count.ToString();  
        }
        public void GroupBy()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            if (UseQuerySyntax)
            {
                sizeGroup = (from prod in Products
                             orderby prod.Size
                             group prod by prod.Size);
            }
            else
            {
                sizeGroup = Products.OrderBy(prod => prod.Size).
                    GroupBy(prod => prod.Size);
                   
            }
            foreach(var group in sizeGroup)
            {
              sb.AppendLine($"Size: {group.Key} count: {group.Count()}");
                foreach(var prod in group)
                {
                    sb.AppendLine($"ProductID: {prod.ProductID}");
                    sb.AppendLine($"Name: {prod.Name}");
                    sb.AppendLine($"color {prod.Color}");
                }
            }
            ResultText = sb.ToString();
        }
        public void GroupByIntoSelect()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<IGrouping<string, Product>> sizeGroup;


            if (UseQuerySyntax)
            {
                sizeGroup = (from prod in Products
                             orderby prod.Size
                             group prod by
                             prod.Size into sizes
                             select sizes);
            }
            else
            {
                sizeGroup = Products.OrderBy(prod => prod.Size).
                    GroupBy(prod => prod.Size);
            }
            foreach (var group in sizeGroup)
            {
                sb.AppendLine($"Size: {group.Key} count: {group.Count()}");
                foreach (var prod in group)
                {
                    sb.AppendLine($"ProductID: {prod.ProductID}");
                    sb.AppendLine($"Name: {prod.Name}");
                    sb.AppendLine($"color {prod.Color}");
                }
            }
            ResultText = sb.ToString();
        }
        public void GroupByOrderByKey()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<IGrouping<string, Product>> sizeGroup;

            if(UseQuerySyntax)
            {
                sizeGroup = (from prod in Products
                             group prod by prod.Size
                             into sizes
                             orderby sizes.Key
                             select sizes);
            }
            else
            {
                sizeGroup = Products.GroupBy(prod => prod.Size).
                    OrderBy(sizes => sizes.Key).Select(sizes => sizes);
            }
            foreach (var group in sizeGroup)
            {
                sb.AppendLine($"Size: {group.Key} count: {group.Count()}");
                foreach (var prod in group)
                {
                    sb.AppendLine($"ProductID: {prod.ProductID}");
                    sb.AppendLine($"Name: {prod.Name}");
                    sb.AppendLine($"color {prod.Color}");
                }
            }
            ResultText = sb.ToString();

        }
    }
    
}
