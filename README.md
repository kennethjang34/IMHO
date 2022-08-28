# IMHO. Well, not so humble actually.

## A platform where you can share your humble opinions regarding the hottest topics with your favourite communities!

Demo Video: https://youtu.be/J2Ynk8flQhk



The application is divided into server and client sides. The server side is mainly built in ASP.NET while the client side built in Angular.

The application supports user authentication using OAuth2.0/OIDC (Google/Okta). Claim-based identity class was chosen so that the server could get the user data from authentication servers easily. Since the client side is designed to give the users feelings of an SPA, the entire authentication process including redirect urls for access and ID tokens of OAuth2.0/OIDC is handled directly by the browser itself. After the authentication is done with a valid access/ID token, the server authorizes the user based on their identities. If it was the user's first time using IMHO, IMHO will automatically create a new account for the user and assign a unique ID to the account. Then the account will be saved to the DB so only that user can perform CRUD actions with his/her own posts.

Once authenticated, users can make posts with title, body, and images with tags in their favourite channels.
A channel is a term for community in IMHO, which defines a scope of each post. Upon creation, a new post is sent to the server through HTML POST method with encoding of multipart/form-data. The multipart/form-data encoding method was used to allow the client side to send images with the posts they are attached to at the same time.  However, When posts are loaded, posts cannot be loaded with its image data in the same HTTP response. Therefore, post data is sent to the user in the JSON array format and then the client side requests the images files attached to those posts through the server's Image processing APIs. To keep the APIs as consistent as possible, much time has been spent to keep them RESTful.



CIP.
Current state of the development:
Working on the UI for channels and tags systems





