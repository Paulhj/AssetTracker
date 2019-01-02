# AssetTracker
Simple .NET Core asset tracking application using OAuth2 and OpenID Connect

Initial Setup Tasks Completed 1st Pull Request (12/28/2018):

1. Created AssetTracker Blank Solution
2. Created Common .NET Core Library (2.2) - Has all common classes and interfaces that can be used across other domain libraries.
3. Created AssetTracker.Core  .NET Core Library (2.2) Holds all the domain classes for the AssetTracker application including Entities, Repositories and Service classes.  User EntityFrameworkCore migrations to create database from domain classess and seed data for testing.
4. Created AssetTracker.Api .NET Core WebApi (2.2) - Added the UserController and OrganizationController classes to the api as a starting point for developement.

2nd Pull Request (1/2/2019):

1. Created AssetTracker.Client .NET Core MVC Web Application (2.2).  Simple MVC application that currently only brings up a list of Assets and allows the user to select the Asset to get the details view.  Created this solution to make it easier to implement and test OpenId Connect and IdentityServer which will be added to the solution in the next pull request.
2. Added migration to add the Organization relationship to the Statues and Types tables in the database so each of those entities will now be associated with an Organization.
3. Added more repository and services classes to the AssetTracker.Core project to support more API functionality.