﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEvaluator;

namespace Tests
{
    public class c
    {
        public double sum(double i, double t)
        {
            var result = 0d;
            return result;
        }

        public double sum(double i, int t)
        {
            var result = 0d;
            return result;
        }

        public int sum(int i, int t)
        {
            var result = 0;
            return result;
        }

        public int sum(int i1, int i2, int i3, int i4, int i5)
        {
            var result = 0;
            return result;
        }


        public double sum(double i, params double[] nums)
        {
            var result = 0d;
            foreach (var num in nums)
            {
                result += num;
            }
            return result;
        }

        public float sum(float i, params float[] nums)
        {
            var result = 0f;
            foreach (var num in nums)
            {
                result += num;
            }
            return result;
        }


        public int yes()
        {
            return 1234;
        }

        public bool no
        {
            get { return false; }
        }

        public int fix(int x)
        {
            return x + 1;
        }

        public int func(Predicate<int> t)
        {
            return t(5) ? 1 : 2;
        }
    }

    public class c2
    {
        public int yes()
        {
            return 1234;
        }

        public bool no
        {
            get { return true; }
        }

        public int fix(int x)
        {
            return x + 1;
        }

        public int sum(params int[] nums)
        {
            var result = 0;
            foreach (var num in nums)
            {
                result -= num;
            }
            return result;
        }

    }

    public class MyClass
    {
        public int X { get; set; }
        public Func<bool> Value { get; set; }
        public void Foo()
        {
            X++;
        }

        public void Foo(int value)
        {
            X += value;
        }

        public int Bar(int value)
        {
            return value * 2;
        }
    }

    public class objHolder
    {
        public bool result { get; set; }
    }


    public class ValueHolder
    {
        public string Value { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var field = new ValueHolder() { Value = "405" };
            var query = new ValueHolder() { Value = "405" };
            var ra11 = Convert.ToInt32(field.Value) >= Convert.ToInt32(query.Value);
            var c11 = new CompiledExpression() { StringToParse = "Convert.ToInt32(field.Value) >= Convert.ToInt32(query.Value)" };
            c11.RegisterType("field", field);
            c11.RegisterType("query", query);
            c11.RegisterDefaultTypes();
            var x11 = c11.Compile();
            var r11 = x11();

            var cc = new CompiledExpression() { StringToParse = "Convert.ToBoolean(obj.result)==true" };
            object obj = new objHolder() { result = true };
            cc.RegisterType("obj", obj);
            cc.RegisterDefaultTypes();
            //var ra22 = Convert.ToBoolean(obj.result) == true;
            var result = cc.Eval();

            var x = new List<String>() { "Hello", "There", "World" };
            dynamic scope = new ExpandoObject();
            scope.x = x;
            var data = new MyClass { Value = () => false };
            var item = new MyClass { Value = () => true };
            scope.data = data;
            scope.item = item;
            scope.i = 1;
            var a = scope.data.Value() && scope.item.Value();
            //var b = !scope.data.Value() || scope.item.Value();

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            var pi = Convert.ToString(3.141592654);
            var xs = 2d;
            var pipi = 3.141592654.ToString();
            var c0 = new CompiledExpression() { StringToParse = "3.141592654.ToString()" };
            var pi2 = c0.Eval();

            var p = scope.x[0];

            // (data.Value && !item.Value) ? 'yes' : 'no'
            var c = new CompiledExpression() { StringToParse = "data.Foo(30 + data.Bar(10))" };
            c.RegisterType("data", data);
            Console.WriteLine(data.X);
            c.Eval();
            //c.Call();
            Console.WriteLine(data.X);

            var c8= new CompiledExpression() { StringToParse = "data.X  + '%'" };
            c8.RegisterType("data", data);
            Console.WriteLine(data.X);
            var cr = c8.Eval();
            Console.WriteLine(data.X);


            var c1 = new CompiledExpression() { StringToParse = "Foo()" };
            var f1 = c1.ScopeCompileCall<MyClass>();
            Console.WriteLine(data.X);
            f1(data);
            Console.WriteLine(data.X);
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            var fr = new CultureInfo("fr-FR");

            var qq = (25.82).ToString("0.00", fr) + "px";
            var test = "(25.82).ToString('0.00') + 'px'";
            var cx = new CompiledExpression() { StringToParse = "int.Parse('25.82', fr)" };
            cx.RegisterType("fr", fr);


            var c2 = new CompiledExpression() { StringToParse = "data.Foo()" };
            var y = 12 + "px";
            var f2 = c2.ScopeCompileCall();
            Console.WriteLine(scope.data.X);
            f2(scope);
            Console.WriteLine(scope.data.X);

            scope.c = new c();

            var c3 = new CompiledExpression() { StringToParse = "c.sum(1,2,3,4,5,6,7,8)" };
            var f3 = c3.ScopeCompile();
            var x3 = f3(scope);

            var c4 = new CompiledExpression() { StringToParse = "c.sum(1,2)" };
            var f4 = c4.ScopeCompile();
            var x4 = f4(scope);

            var c5 = new CompiledExpression() { StringToParse = "c.sum(1.0d,2.0d)" };
            var f5 = c5.ScopeCompile();
            var x5 = f5(scope);

            var c6 = new CompiledExpression() { StringToParse = "c.sum(1,2.0d)" };
            var f6 = c6.ScopeCompile();
            var x6 = f6(scope);

            Console.ReadLine();
        }
    }
}
