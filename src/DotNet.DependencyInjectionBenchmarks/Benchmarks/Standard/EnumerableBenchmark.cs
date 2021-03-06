﻿using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DotNet.DependencyInjectionBenchmarks.Classes;
using DotNet.DependencyInjectionBenchmarks.Containers;

namespace DotNet.DependencyInjectionBenchmarks.Benchmarks.Standard
{
    [BenchmarkCategory("Standard")]
    public class EnumerableBenchmark : StandardBenchmark
    {
        public static string Description =>
            @"Resolves one object that depends on an enumerable containing 5 instances";

        protected override IEnumerable<RegistrationDefinition> Definitions
        {
            get
            {
                yield return new RegistrationDefinition { ExportType = typeof(IEnumerableService), ActivationType = typeof(EnumerableService1), RegistrationMode = RegistrationMode.Multiple };
                yield return new RegistrationDefinition { ExportType = typeof(IEnumerableService), ActivationType = typeof(EnumerableService2), RegistrationMode = RegistrationMode.Multiple };
                yield return new RegistrationDefinition { ExportType = typeof(IEnumerableService), ActivationType = typeof(EnumerableService3), RegistrationMode = RegistrationMode.Multiple };
                yield return new RegistrationDefinition { ExportType = typeof(IEnumerableService), ActivationType = typeof(EnumerableService4), RegistrationMode = RegistrationMode.Multiple };
                yield return new RegistrationDefinition { ExportType = typeof(IEnumerableService), ActivationType = typeof(EnumerableService5), RegistrationMode = RegistrationMode.Multiple };
                yield return new RegistrationDefinition { ExportType = typeof(ISingletonService), ActivationType = typeof(SingletonService) };
                yield return new RegistrationDefinition { ExportType = typeof(ITransientService), ActivationType = typeof(TransientService) };

                yield return new RegistrationDefinition { ExportType = typeof(IImportEnumerableService), ActivationType = typeof(ImportEnumerableService) };
            }
        }

        protected override void ExecuteBenchmark(IResolveScope scope)
        {
            if (scope.Resolve<IImportEnumerableService>().Services.Count() != 5)
            {
                throw new Exception("Count does not equal 5");
            }
        }

        [Benchmark]
        [BenchmarkCategory(nameof(Autofac))]
        public void Autofac()
        {
            ExecuteBenchmark(AutofacContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(CastleWindsor))]
        public void CastleWindsor()
        {
            ExecuteBenchmark(CastleWindsorContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(DryIoc))]
        public void DryIoc()
        {
            ExecuteBenchmark(DryIocContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(Grace))]
        public void Grace()
        {
            ExecuteBenchmark(GraceContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(LightInject))]
        public void LightInject()
        {
            ExecuteBenchmark(LightInjectContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(MicrosoftDependencyInjection))]
        public void MicrosoftDependencyInjection()
        {
            ExecuteBenchmark(MicrosoftDependencyInjectionContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(NInject))]
        public void NInject()
        {
            ExecuteBenchmark(NInjectContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(SimpleInjector))]
        public void SimpleInjector()
        {
            ExecuteBenchmark(SimpleInjectorContainer);
        }

        [Benchmark]
        [BenchmarkCategory(nameof(StructureMap))]
        public void StructureMap()
        {
            ExecuteBenchmark(StructureMapContainer);
        }

    }
}
