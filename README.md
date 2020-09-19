Project 1: Fractal-Landscape

Group member: YIFAN CAI, HANG ZOU, ZEHONGLI

To use Diamond -Square algorithm to randomly generate all point for terrain, first in our script "DiamondSquareTerrain.cs", we create 128*128 point across the map and initialize point's location and store their neighbour point with them as triangles, then change the height of four conner point value to a random number in a range, then start to use the Diamond-Square algorithm with its diamond and square to generate height and plus a random value plus on height in the setting range, to create a group of point with different height which will use to generate terrain.

Then we traverse all the point to figure out their colours and store their colour, we combine all the points with their vertices, uvs, colours and triangles to create a mesh, after that we calculate the normals for generating smooth shadow and bounds for bounding. after all, we can render the terrain with colourful texture.
