# AssetTracker
Simple .NET Core asset tracking application using OAuth2 and OpenID Connect

Initial Setup Tasks Completed:

1. Created AssetTracker Blank Solution
2. Created Common .NET Core Library (2.2) - Has all common classes and interfaces that can be used across other domain libraries.
3. Created AssetTracker.Core  .NET Core Library (2.2) Holds all the domain classes for the AssetTracker application including Entities, Repositories and Service classes.  User EntityFrameworkCore migrations to create database from domain classess and seed data for testing.
4. Created AssetTracker.Api .NET Core WebApi (2.2) - Added the UserController and OrganizationController classes to the api as a starting point for developement.
