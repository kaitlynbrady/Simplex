﻿using RaikesSimplexService.InsertTeamNameHere;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RaikesSimplexService.DataModel;


namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for SolverTest and is intended
    ///to contain all SolverTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SolverTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void Simple2PhaseFromPaper()
        {
            #region Arrange
            var target = new Solver();

            var lc1 = new LinearConstraint()
            {
                Coefficients = new double[2] { 1, 1 },
                Relationship = Relationship.GreaterThanOrEquals,
                Value = 4
            };

            var lc2 = new LinearConstraint()
            {
                Coefficients = new double[2] { 1, -1 },
                Relationship = Relationship.GreaterThanOrEquals,
                Value = 1
            };

            var lc3 = new LinearConstraint()
            {
                Coefficients = new double[2] { -1, 2 },
                Relationship = Relationship.GreaterThanOrEquals,
                Value = -1
            };


            var constraints = new List<LinearConstraint>() { lc1, lc2, lc3 };

            var goal = new Goal()
            {
                Coefficients = new double[2] { 1, 2 },
                ConstantTerm = 0
            };

            var model = new Model()
            {
                Constraints = constraints,
                Goal = goal,
                GoalKind = GoalKind.Minimize
            };

            var expected = new Solution()
            {
                Decisions = new double[2] { 3, 1 },
                Quality = SolutionQuality.Optimal,
                AlternateSolutionsExist = false,
                OptimalValue = 5
            };
            #endregion

            //Act
            var actual = target.Solve(model);

            //Assert
            CollectionAssert.AreEqual(expected.Decisions, actual.Decisions);
            Assert.AreEqual(expected.Quality, actual.Quality);
            Assert.AreEqual(expected.AlternateSolutionsExist, actual.AlternateSolutionsExist);
        }

        /// <summary>
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void ExampleSolveTest()
        {
            #region Arrange
            var target = new Solver();            

            var lc1 = new LinearConstraint()
            {
                Coefficients = new double[2] { 8, 12 },
                Relationship = Relationship.GreaterThanOrEquals,
                Value = 24
            };

            var lc2 = new LinearConstraint()
            {
                Coefficients = new double[2] { 12, 12 },
                Relationship = Relationship.GreaterThanOrEquals,
                Value = 36
            };

            var lc3 = new LinearConstraint()
            {
                Coefficients = new double[2] { 2, 1 },
                Relationship = Relationship.GreaterThanOrEquals,
                Value = 4
            };

            var lc4 = new LinearConstraint()
            {
                Coefficients = new double[2] { 1, 1 },
                Relationship = Relationship.LessThanOrEquals,
                Value = 5
            };

            var constraints = new List<LinearConstraint>() {lc1, lc2, lc3, lc4};

            var goal = new Goal() 
            { 
                Coefficients = new double[2] { 0.2, 0.3 },
                ConstantTerm = 0
            };           

            var model = new Model()
            {
                Constraints = constraints,
                Goal = goal,
                GoalKind = GoalKind.Minimize
            };
            
            var expected = new Solution()
            {
                Decisions = new double[2] { 3, 0 },
                Quality = SolutionQuality.Optimal,
                AlternateSolutionsExist = false,
                OptimalValue = 0.6
            };
            #endregion

            //Act
            var actual = target.Solve(model);

            //Assert
            CollectionAssert.AreEqual(expected.Decisions, actual.Decisions);
            Assert.AreEqual(expected.Quality, actual.Quality);
            Assert.AreEqual(expected.AlternateSolutionsExist, actual.AlternateSolutionsExist);
        }




        //http://optlab.mcmaster.ca/~feng/4O03/LP.Degeneracy.pdf
        //degeneracy
        [TestMethod()]
        public void DegeneracyTest()
        {
            #region Arrange
            var target = new Solver();

            var lc1 = new LinearConstraint()
            {
                Coefficients = new double[3] { 1, 1, 0 },
                Relationship = Relationship.LessThanOrEquals,
                Value = 1
            };

            var lc2 = new LinearConstraint()
            {
                Coefficients = new double[3] { 0, -1, 1 },
                Relationship = Relationship.LessThanOrEquals,
                Value = 0
            };

            var constraints = new List<LinearConstraint>() { lc1, lc2 };

            var goal = new Goal()
            {
                Coefficients = new double[3] { 1, 1, 1 },
                ConstantTerm = 0
            };

            var model = new Model()
            {
                Constraints = constraints,
                Goal = goal,
                GoalKind = GoalKind.Maximize
            };

            var expected = new Solution()
            {
                Decisions = new double[3] { 0, 1, 1 },
                Quality = SolutionQuality.Optimal,
                AlternateSolutionsExist = false,
                OptimalValue = 2
            };
            #endregion

            //Act
            var actual = target.Solve(model);

            //Assert
            CollectionAssert.AreEqual(expected.Decisions, actual.Decisions);
            Assert.AreEqual(expected.Quality, actual.Quality);
            Assert.AreEqual(expected.AlternateSolutionsExist, actual.AlternateSolutionsExist);
        }


   
        //two phase
        [TestMethod()]
        public void TwoPhaseClassTest()
        {
            #region Arrange
            var target = new Solver();

            var lc1 = new LinearConstraint()
            {
                Coefficients = new double[2] { 1, 1 },
                Relationship = Relationship.LessThanOrEquals,
                Value = 35
            };

            var lc2 = new LinearConstraint()
            {
                Coefficients = new double[2] { 1, 2 },
                Relationship = Relationship.LessThanOrEquals,
                Value = 38
            };


            var lc3 = new LinearConstraint()
            {
                Coefficients = new double[2] { 2, 2 },
                Relationship = Relationship.LessThanOrEquals,
                Value = 50
            };

            var constraints = new List<LinearConstraint>() { lc1, lc2, lc3 };

            var goal = new Goal()
            {
                Coefficients = new double[2] { 350, 450 },
                ConstantTerm = 0
            };

            var model = new Model()
            {
                Constraints = constraints,
                Goal = goal,
                GoalKind = GoalKind.Maximize
            };

            var expected = new Solution()
            {
                Decisions = new double[2] { 12, 13 },
                Quality = SolutionQuality.Optimal,
                AlternateSolutionsExist = false,
                OptimalValue = 10050 // 12*350 + 13*450
            };

            var actual = target.Solve(model);

            #endregion
            ////Act
            
            ////Assert
            CollectionAssert.AreEqual(expected.Decisions, actual.Decisions);
            Assert.AreEqual(expected.Quality, actual.Quality);
            Assert.AreEqual(expected.AlternateSolutionsExist, actual.AlternateSolutionsExist);
        }

    }
}
