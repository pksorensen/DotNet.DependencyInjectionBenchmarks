﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using DotNet.DependencyInjectionBenchmarks.Classes;
using DotNet.DependencyInjectionBenchmarks.Containers;

namespace DotNet.DependencyInjectionBenchmarks.Benchmarks.Standard
{
    [BenchmarkCategory("Standard")]
    public class GenericObjectBenchmark : BaseBenchmark
    {
        [GlobalSetup]
        public void Setup()
        {
            var definitions = Definitions().ToArray();

            var warmup = new Action<IResolveScope>[]
            {
                scope => scope.Resolve<IGenericObjectService<ISmallObjectService>>()
            };
            
            SetupContainerForTest(CreateAutofacContainer(), definitions, warmup);
            SetupContainerForTest(CreateCastleWindsorContainer(), definitions, warmup);
            SetupContainerForTest(CreateDryIocContainer(), definitions, warmup);
            SetupContainerForTest(CreateGraceContainer(), definitions, warmup);
            SetupContainerForTest(CreateLightInjectContainer(), definitions, warmup);
            SetupContainerForTest(CreateNInjectContainer(), definitions, warmup);
            SetupContainerForTest(CreateSimpleInjectorContainer(), definitions, warmup);
            SetupContainerForTest(CreateStructureMapContainer(), definitions, warmup);
        }

        public static IEnumerable<RegistrationDefinition> Definitions()
        {
            foreach (var registrationDefinition in SmallObjectBenchmark.Definitions())
            {
                yield return registrationDefinition;
            }

            yield return new RegistrationDefinition { ExportType = typeof(IGenericObjectService<>), ActivationType = typeof(GenericObjectService<>) };
        }


        #region Benchmarks

        [Benchmark]
        [BenchmarkCategory("Autofac")]
        public void Autofac()
        {
            ExecuteBenchmark(AutofacContainer);
        }

        [Benchmark]
        [BenchmarkCategory("CastleWindsor")]
        public void CastleWindsor()
        {
            ExecuteBenchmark(CastleWindsorContainer);
        }

        [Benchmark]
        [BenchmarkCategory("DryIoc")]
        public void DryIoc()
        {
            ExecuteBenchmark(DryIocContainer);
        }

        [Benchmark]
        [BenchmarkCategory("Grace")]
        public void Grace()
        {
            ExecuteBenchmark(GraceContainer);
        }

        [Benchmark]
        [BenchmarkCategory("LightInject")]
        public void LightInject()
        {
            ExecuteBenchmark(LightInjectContainer);
        }
        
        [Benchmark]
        [BenchmarkCategory("NInject")]
        public void NInject()
        {
            ExecuteBenchmark(NInjectContainer);
        }

        [Benchmark]
        [BenchmarkCategory("SimpleInjector")]
        public void SimpleInjector()
        {
            ExecuteBenchmark(SimpleInjectorContainer);
        }

        [Benchmark]
        [BenchmarkCategory("StructureMap")]
        public void StructureMap()
        {
            ExecuteBenchmark(StructureMapContainer);
        }

        private void ExecuteBenchmark(IResolveScope scope)
        {
            scope.Resolve(typeof(IGenericObjectService<ISmallObjectService>));
        }

        #endregion


    }
}
