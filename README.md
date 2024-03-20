This is Legit security home assigment

In this assigment we need to focus on 'https://main.d2t1pk7fjag8u6.amplifyapp.com/' as shopping cart website, write full lifcecycle using my favorite tools.

Here's the tech stack: 
1. Programing lanugage: C#
2. Testing mechanizem: Playwright & Nunit
3. CI tool: GitHub

Here's brief about the project: 
1. ConfigFIle -> We will store all the configuration files in this folder.
2. Contract -> We will store all the object, DTO's and other data model in this folder.
3. Exceptions -> We will store here all the types of exceptions we will need for this project.
4. Steps -> We will store here all the steps or minor behaviour that need in order to keep our code SOILD as possible.
5. Test -> We will store all the tests in this folder.
6. TestBase.cs -> This is the base class for all the tests, we will write down in this file the common behaviour across our tests.

CI implementations:
in this path 'C:\Project\LegitTestingTask\.github\workflows' we have to Yaml files:
1. playwright - CI: this file responsible to execute the tests every push to master.
2. schedule - Nightly: this file responsible to execute the tests every night.

Execution and configuration 
In order to execute the test, just open CMD and run 'dotnet run test'
In order to config the execution, please edit this file 'PlaywrightTests/ConfigFiles/ConfigFile.json'
