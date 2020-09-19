# Project 1: Fractal-Landscape

## Group member: YIFAN CAI, HANG ZOU, ZEHONG LI

## Diamond-Square algorithm
To use Diamond -Square algorithm to randomly generate all point for terrain, first in our script "DiamondSquareTerrain.cs", we create 128*128 point across the map and initialize point's location and store their neighbour point with them as triangles, then change the height of four conner point value to a random number in a range, then start to use the Diamond-Square algorithm with its diamond and square to generate height and plus a random value on height in the setting range, to create a group of point with different height which will use to generate terrain.

Then we traverse all the point to figure out their colours and store their colour, we combine all the points with their vertices, uvs, colours and triangles to create a mesh, after that we calculate the normals for generating smooth shadow and bounds for bounding. after all, we can render the terrain with colourful texture.

## Camera motion
In order to achieve the 'flight simulation' style on camera control, we apply movement and rotation with camera parameters roll, pitch, and yaw. The user could interact with the camera with input devices(keyboard, and mouse) by positioning through the coordinates. We reset the camera position by translating to a fixed Vector3 coordinate associate with the map size. To achieve the camera able to reset on the first frame and the re-generated terrain, we reset in the Start() and the condition when the user input when pressing the key 'space'. The rotation represents as Euler angles in degrees relative to the parent transform's rotation, so that the user could rotate the camera with any angles including rolled inverted flight. For the map size that the camera able to visit, we set the scale using Mathf.Clamp to limit the range on the x, y, and z-axis.

## Illumination of terrain and water wave
For the illumination of the terrain, we use a diffuse shader. In the shader we use two Pass to shade the terrain, the first one is to shader the terrain with the color which is already defined in the mesh, and shade the diffuse light effect to the terrain. The second one is to shade the moon light into the terrain.

To achieve a realistic wave effect and illumination, we ues the water texture and normal texture to achieve the specular illumination. We also made the vertices move up and down at it's plane position to achieve the wave effect. For each frame, we will recalculate the normal of the water surface, and pass the normal data into the shader to generate the shadow of the wave.
