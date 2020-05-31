using NUnit.Framework;
using Timespawn.Core.DOTS.Tween;
using Timespawn.Core.DOTS.Tween.Systems;
using Timespawn.Core.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Timespawn.Core.Tests
{
    [TestFixture]
    public class TweenTests : ECSTestFixture
    {
        public struct UpdateSystemTestCase<T>
        {
            public T Start;
            public T End;
            public float Percentage;
            public T Expected;

            public UpdateSystemTestCase(T start, T end, float percentage, T expected)
            {
                Start = start;
                End = end;
                Percentage = percentage;
                Expected = expected;
            }

            public override string ToString()
            {
                return $"From {Start} to {End} with {Percentage} percentage = {Expected}";
            }
        }

        public struct CompleteSystemTestCase
        {
            public float NormalizedTime;
            public bool IsPingPong;
            public bool IsReverting;
            public short LoopNum;
            public bool ExpectedDestroy;
            public bool ExpectedReverting;
            public float ExpectedElapsedTime;
            public short ExpectedLoopNum;

            public CompleteSystemTestCase(float normalizedTime, bool isPingPong, bool isReverting, short loopNum, bool expectedDestroy, bool expectedReverting, float expectedElapsedTime, short expectedLoopNum)
            {
                NormalizedTime = normalizedTime;
                IsPingPong = isPingPong;
                IsReverting = isReverting;
                LoopNum = loopNum;
                ExpectedDestroy = expectedDestroy;
                ExpectedReverting = expectedReverting;
                ExpectedElapsedTime = expectedElapsedTime;
                ExpectedLoopNum = expectedLoopNum;
            }

            public override string ToString()
            {
                return NormalizedTime >= 1.0f ? "Complete" : "Incomplete";
            }
        }

        private const float COMPLETE_SYSTEM_TEST_INITIAL_ELAPSED_TIME = 1.0f;

        private static readonly UpdateSystemTestCase<float3>[] MovementAndScaleUpdateSystemTestCases =
        {
            new UpdateSystemTestCase<float3>(float3.zero, new float3(1.0f, 2.0f, 3.0f), 0.6f, new float3(0.6f, 1.2f, 1.8f)), 
        };

        private static readonly UpdateSystemTestCase<quaternion>[] RotationUpdateSystemTestCases =
        {
            new UpdateSystemTestCase<quaternion>(
                quaternion.identity, 
                quaternion.EulerXYZ(2.0f, -0.6f, 2.5f), 
                0.6f, 
                new quaternion(-0.3177298f, -0.5589807f, -0.4457927f, 0.6227818f)), 
        };

        private static readonly CompleteSystemTestCase[] CompleteSystemTestCases =
        {
            new CompleteSystemTestCase(0.5f, false, false, 0, false, false, COMPLETE_SYSTEM_TEST_INITIAL_ELAPSED_TIME, 0), 
            new CompleteSystemTestCase(1.0f, false, false, 0, true, false, COMPLETE_SYSTEM_TEST_INITIAL_ELAPSED_TIME, 0), 
        };

        [TestCase(EaseType.SmoothStep2, 5.0f, 10.0f, false, 0.016f, 5.016f, 0.5016f, 0.5023999918f)]
        [TestCase(EaseType.SmoothStep2, 5.0f, 10.0f, true, 0.016f, 4.984f, 0.4984f, 0.49760000819f)]
        public void UpdateTweenState(
            EaseType type,
            float elapsedTime,
            float duration,
            bool isReverting, 
            float deltaTime, 
            float expectedElapsedTime, 
            float expectedNormalizedTime, 
            float expectedPercentage)
        {
            TweenState state = new TweenState(type, duration, false, 0);
            state.ElapsedTime = elapsedTime;
            state.IsReverting = isReverting;

            TweenSystemUtils.UpdateTweenState(ref state, deltaTime);

            TestUtils.AreApproximatelyEqual(expectedElapsedTime, state.ElapsedTime, "Incorrect elapsed time.");
            TestUtils.AreApproximatelyEqual(expectedNormalizedTime, state.NormalizedTime, "Incorrect normalized time.");
            TestUtils.AreApproximatelyEqual(expectedPercentage, state.Percentage, "Incorrect percentage.");
        }

        [Test]
        public void TweenMovementUpdateSystem([ValueSource(nameof(MovementAndScaleUpdateSystemTestCases))] UpdateSystemTestCase<float3> testCase)
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(Translation), typeof(TweenMovementData));
            ActiveEntityManager.SetComponentData(entity, new TweenMovementData
            {
                Start = testCase.Start,
                End = testCase.End,
                State = new TweenState
                {
                    Percentage = testCase.Percentage,
                },
            });

            ActiveWorld.GetOrCreateSystem<TweenMovementUpdateSystem>().Update();

            Translation translation = ActiveEntityManager.GetComponentData<Translation>(entity);
            TestUtils.AreApproximatelyEqualFloat3(testCase.Expected, translation.Value, "Incorrect new position of the entity.");
        }

        [Test]
        public void TweenRotationUpdateSystem([ValueSource(nameof(RotationUpdateSystemTestCases))] UpdateSystemTestCase<quaternion> testCase)
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(Rotation), typeof(TweenRotationData));
            ActiveEntityManager.SetComponentData(entity, new TweenRotationData
            {
                Start = testCase.Start,
                End = testCase.End,
                State = new TweenState
                {
                    Percentage = testCase.Percentage,
                },
            });

            ActiveWorld.GetOrCreateSystem<TweenRotationUpdateSystem>().Update();

            Rotation rotation = ActiveEntityManager.GetComponentData<Rotation>(entity);
            TestUtils.AreApproximatelyEqual(testCase.Expected, rotation.Value, "Incorrect new rotation of the entity.");
        }

        [Test]
        public void TweenScaleUpdateSystem([ValueSource(nameof(MovementAndScaleUpdateSystemTestCases))] UpdateSystemTestCase<float3> testCase)
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(NonUniformScale), typeof(TweenScaleData));
            ActiveEntityManager.SetComponentData(entity, new TweenScaleData
            {
                Start = testCase.Start,
                End = testCase.End,
                State = new TweenState
                {
                    Percentage = testCase.Percentage,
                },
            });

            ActiveWorld.GetOrCreateSystem<TweenScaleUpdateSystem>().Update();

            NonUniformScale scale = ActiveEntityManager.GetComponentData<NonUniformScale>(entity);
            TestUtils.AreApproximatelyEqualFloat3(testCase.Expected, scale.Value, "Incorrect new scale of the entity.");
        }

        [Test]
        public void TweenMovementCompleteSystem([ValueSource(nameof(CompleteSystemTestCases))] CompleteSystemTestCase testCase)
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(TweenMovementData));
            ActiveEntityManager.SetComponentData(entity, new TweenMovementData
            {
                State = new TweenState
                {
                    ElapsedTime = COMPLETE_SYSTEM_TEST_INITIAL_ELAPSED_TIME,
                    NormalizedTime = testCase.NormalizedTime,
                    IsPingPong = testCase.IsPingPong,
                    IsReverting = testCase.IsReverting,
                    LoopNum = testCase.LoopNum,
                }
            });

            ActiveWorld.GetOrCreateSystem<TweenMovementCompleteSystem>().Update();

            TweenMovementData data = ActiveEntityManager.GetComponentData<TweenMovementData>(entity);
            Assert.AreEqual(testCase.ExpectedReverting, data.State.IsReverting, "Incorrect IsReverting flag.");
            Assert.AreEqual(testCase.ExpectedElapsedTime, data.State.ElapsedTime, "Incorrect ElapsedTime.");
            Assert.AreEqual(testCase.ExpectedLoopNum, data.State.LoopNum, "Incorrect LoopNum.");

            if (testCase.ExpectedDestroy)
            {
                ActiveWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().Update();
                bool hasData = ActiveEntityManager.HasComponent<TweenMovementData>(entity);
                Assert.IsFalse(hasData, "Tween data should have been removed.");

                ActiveWorld.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>().Update();
                bool hasCompleteTag = ActiveEntityManager.HasComponent<TweenMovementCompleteTag>(entity);
                Assert.IsTrue(hasCompleteTag, "Tween complete tag should have been added.");

                ActiveWorld.GetOrCreateSystem<TweenMovementCompleteSystem>().Update();
                ActiveWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().Update();
                bool hasCompleteTagAfter = ActiveEntityManager.HasComponent<TweenMovementCompleteTag>(entity);
                Assert.IsFalse(hasCompleteTagAfter, "Tween complete tag should have been removed.");
            }
        }

        [Test]
        public void TweenRotationCompleteSystem([ValueSource(nameof(CompleteSystemTestCases))] CompleteSystemTestCase testCase)
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(TweenRotationData));
            ActiveEntityManager.SetComponentData(entity, new TweenRotationData
            {
                State = new TweenState
                {
                    ElapsedTime = COMPLETE_SYSTEM_TEST_INITIAL_ELAPSED_TIME,
                    NormalizedTime = testCase.NormalizedTime,
                    IsPingPong = testCase.IsPingPong,
                    IsReverting = testCase.IsReverting,
                    LoopNum = testCase.LoopNum,
                }
            });

            ActiveWorld.GetOrCreateSystem<TweenRotationCompleteSystem>().Update();

            TweenRotationData data = ActiveEntityManager.GetComponentData<TweenRotationData>(entity);
            Assert.AreEqual(testCase.ExpectedReverting, data.State.IsReverting, "Incorrect IsReverting flag.");
            Assert.AreEqual(testCase.ExpectedElapsedTime, data.State.ElapsedTime, "Incorrect ElapsedTime.");
            Assert.AreEqual(testCase.ExpectedLoopNum, data.State.LoopNum, "Incorrect LoopNum.");

            if (testCase.ExpectedDestroy)
            {
                ActiveWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().Update();
                bool hasData = ActiveEntityManager.HasComponent<TweenRotationData>(entity);
                Assert.IsFalse(hasData, "Tween data should have been removed.");

                ActiveWorld.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>().Update();
                bool hasCompleteTag = ActiveEntityManager.HasComponent<TweenRotationCompleteTag>(entity);
                Assert.IsTrue(hasCompleteTag, "Tween complete tag should have been added.");

                ActiveWorld.GetOrCreateSystem<TweenRotationCompleteSystem>().Update();
                ActiveWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().Update();
                bool hasCompleteTagAfter = ActiveEntityManager.HasComponent<TweenRotationCompleteTag>(entity);
                Assert.IsFalse(hasCompleteTagAfter, "Tween complete tag should have been removed.");
            }
        }

        [Test]
        public void TweenScaleCompleteSystem([ValueSource(nameof(CompleteSystemTestCases))] CompleteSystemTestCase testCase)
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(TweenScaleData));
            ActiveEntityManager.SetComponentData(entity, new TweenScaleData
            {
                State = new TweenState
                {
                    ElapsedTime = COMPLETE_SYSTEM_TEST_INITIAL_ELAPSED_TIME,
                    NormalizedTime = testCase.NormalizedTime,
                    IsPingPong = testCase.IsPingPong,
                    IsReverting = testCase.IsReverting,
                    LoopNum = testCase.LoopNum,
                }
            });

            ActiveWorld.GetOrCreateSystem<TweenScaleCompleteSystem>().Update();

            TweenScaleData data = ActiveEntityManager.GetComponentData<TweenScaleData>(entity);
            Assert.AreEqual(testCase.ExpectedReverting, data.State.IsReverting, "Incorrect IsReverting flag.");
            Assert.AreEqual(testCase.ExpectedElapsedTime, data.State.ElapsedTime, "Incorrect ElapsedTime.");
            Assert.AreEqual(testCase.ExpectedLoopNum, data.State.LoopNum, "Incorrect LoopNum.");

            if (testCase.ExpectedDestroy)
            {
                ActiveWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().Update();
                bool hasData = ActiveEntityManager.HasComponent<TweenScaleData>(entity);
                Assert.IsFalse(hasData, "Tween data should have been removed.");

                ActiveWorld.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>().Update();
                bool hasCompleteTag = ActiveEntityManager.HasComponent<TweenScaleCompleteTag>(entity);
                Assert.IsTrue(hasCompleteTag, "Tween complete tag should have been added.");

                ActiveWorld.GetOrCreateSystem<TweenScaleCompleteSystem>().Update();
                ActiveWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().Update();
                bool hasCompleteTagAfter = ActiveEntityManager.HasComponent<TweenScaleCompleteTag>(entity);
                Assert.IsFalse(hasCompleteTagAfter, "Tween complete tag should have been removed.");
            }
        }

        [Test]
        public void TweenMovementPause()
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(Translation), typeof(TweenMovementData));
            ActiveEntityManager.SetComponentData(entity, new TweenMovementData
            {
                State = new TweenState(EaseType.Linear, 10.0f, false, 0),
            });

            TweenUtils.PauseEntity(entity);

            ActiveWorld.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>().Update();
            ActiveWorld.GetOrCreateSystem<TweenMovementEaseSystem>().Update();
            ActiveWorld.GetOrCreateSystem<TweenMovementUpdateSystem>().Update();

            TweenMovementData tweenData = ActiveEntityManager.GetComponentData<TweenMovementData>(entity);
            Translation translation = ActiveEntityManager.GetComponentData<Translation>(entity);

            TestUtils.AreApproximatelyEqual(0.0f, tweenData.State.ElapsedTime, "Paused entity state shouldn't be updated.");
            TestUtils.AreApproximatelyEqualFloat3(float3.zero, translation.Value, "Paused entity shouldn't be updated.");
        }

        [Test]
        public void TweenRotationPause()
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(Rotation), typeof(TweenRotationData));
            ActiveEntityManager.SetComponentData(entity, new Rotation
            {
                Value = quaternion.identity,
            });
            ActiveEntityManager.SetComponentData(entity, new TweenRotationData
            {
                State = new TweenState(EaseType.Linear, 10.0f, false, 0),
            });

            TweenUtils.PauseEntity(entity);

            ActiveWorld.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>().Update();
            ActiveWorld.GetOrCreateSystem<TweenRotationEaseSystem>().Update();
            ActiveWorld.GetOrCreateSystem<TweenRotationUpdateSystem>().Update();

            TweenRotationData tweenData = ActiveEntityManager.GetComponentData<TweenRotationData>(entity);
            Rotation rotation = ActiveEntityManager.GetComponentData<Rotation>(entity);

            TestUtils.AreApproximatelyEqual(0.0f, tweenData.State.ElapsedTime, "Paused entity state shouldn't be updated.");
            TestUtils.AreApproximatelyEqual(quaternion.identity, rotation.Value, "Paused entity shouldn't be updated.");
        }

        [Test]
        public void TweenScalePause()
        {
            Entity entity = ActiveEntityManager.CreateEntity(typeof(NonUniformScale), typeof(TweenScaleData));
            ActiveEntityManager.SetComponentData(entity, new TweenScaleData
            {
                State = new TweenState(EaseType.Linear, 10.0f, false, 0),
            });

            TweenUtils.PauseEntity(entity);

            ActiveWorld.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>().Update();
            ActiveWorld.GetOrCreateSystem<TweenScaleEaseSystem>().Update();
            ActiveWorld.GetOrCreateSystem<TweenScaleUpdateSystem>().Update();

            TweenScaleData tweenData = ActiveEntityManager.GetComponentData<TweenScaleData>(entity);
            NonUniformScale scale = ActiveEntityManager.GetComponentData<NonUniformScale>(entity);

            TestUtils.AreApproximatelyEqual(0.0f, tweenData.State.ElapsedTime, "Paused entity state shouldn't be updated.");
            TestUtils.AreApproximatelyEqualFloat3(float3.zero, scale.Value, "Paused entity shouldn't be updated.");
        }
    }
}