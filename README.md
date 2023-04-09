# Homefinder Exam Project

The website will handle ads for properties and land. There will be two websites, one for advertising and one for administration.

Design-wise, both pages will directly go to indexing and filtering when entered and have a profile tab in the top right corner.

The main difference between the pages will be that the administration website will require logging in directly, while the ad page will have it as an optional alternative in the top right corner where the profile tab will then be displayed.

The workflow to be used is continuous delivery, where the deployment part will be manual via a push to the master branch, which will be connected to Azure via Git actions. However, nothing will be uploaded to Azure until an MVP is achieved.

# The following items will be included in the MVP

1 Ad page

* As an unregistered user, one should be able to search and filter address and price to find the right ad.
* As an unregistered user, one should be able to get information about an object's price, square meters, property type (e.g., townhouse), and address via an ad.

2 Administration page:

* As an unregistered administrator, I want to be able to log in to be considered an authorized administrator.
* As a logged-in administrator, I want to be able to create and delete ads.

3 Both pages:

* Responsive pages that should work on desktop, tablet, and mobile.

4 REST API

* An api that handles crud operations for advertisements and users(Admin)

# Ideas after MVP is done

The idea after this is to implement login on the ad page where one can be either a real estate agent or user. Logged-in users will enable additional functionality for themselves by being able to like and express interest in ads, for example.

The idea with real estate agents is to lift the task of creating ads from the administrator and also be able to keep track of registered users who are interested in the ads they have posted.

Image management will also be introduced on the site so that one can conveniently upload images and replace existing ones when needed. This is done as a real estate agent.

A final step if I finish everything and have time left will be to enable the websites to work offline, in other words, to make it a progressive web app.
