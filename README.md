# unity-system-testing

## Table Of Contents
* [Overview](#overview)
* [Platformer](#platformer-test)
* [Shader Test 01](#shader-test-01)
* [Shader Test 02](#shader-test-02)
* [Dialogue Test](#dialogue-test)

## Overview
Before starting my final game project for college later this year, I wanted to create different mini-levels in Unity, each of them intending to test my ability to create a certain feature. One level may have a specific type of combat, one level may have a dialogue system, etc.

An additional reason for this project is just to test some more advanced systems on their own, before potentially implementing them into actual projects. I am not overly proficient with Unity so I wanted to learn some more complex parts of the tool.

## Platformer Test

#### Player Movement

Having not used Unity for quite some time, I wanted to start with a very basic project to refresh my mind. While it is only a small script, implementing the basic movement helped me remember the C# syntax. I kept using '#' for comments, instead of using '//'.

#### Booster Pads
To challenge myself a bit further, although still very simple, I added two new types of ground. One makes the player run faster, the other makes the player jump higher. I also added a small buffer so that you continue to run after leaving that platform. The buffer is smaller for the jump boost but it is still there.

#### Camera Movement
Although it is quite a basic concept, I have always struggled a bit with smooth camera movement. However, having created a game in another engine recently (GameMaker Studio 2), I applied the same logic to this project and it was a lot more simple than before.

#### Result
![Platformer Test Gif](./files/platformerTest.gif)

## Shader Test 01
I have never really though about using shaders in Unity before. In a recent venture into GameMaker Studio 2, I began to look into shaders a bit more (although never ended up using them). Shaders in GameMaker have their own scripting language, and I was expecting a similar thing in Unity. However, shaders in Unity are created in a shader graph, which is a lot easier to use than I originally thought. I created some basic gameplay and added a red flash when the player is hit, and a dissolve effect (ripped straight from a [Brackeys video](https://youtu.be/5dzGj9k8Qy8) when the player dies.

I did go a tad overboard with the shaders, and the gameplay and collisions are far from perfect. But the focus was learning how to setup up and create basic shaders, so I wasn't too worried about perfection.

#### Result
![Shader Test Gif](./files/shaderTest.gif)

## Shader Test 02

I had a lot of fun making the first shader test. However, I had essentially copied the process from a video and I wanted to try and make something on my own. I was a bit stuck for ideas though. I thought I would start with looking at what nodes are available in the shader graph and I stumbled onto a Voronoi, of which I was vaguely aware of. After fumbling around with the node graph I ended up deciding that I would just try to make a weird psychedelic visual thing. I wanted the texture to move and shift so I created a script. And to be honest, most of the time was spent developing the script and getting the values to shift smoothly but changing speed and direction. 

I eventually came up with something that I was happy with, but there are a few problems - mainly with the colours. The colour was intended to shift, similar to the position and angle. But as I was adding onto the R, G, & B values individually, they would all sort of move together. This resulting in the colour just shifting from white, to grey, to black, etc. And also, when the values reach 0, they just shift back to 255 which is quite jarring. But I had spent too much time on this section of the project so I decided to move on.

#### Results
![Version 1 Gif](./files/shaderTest02_ver1.gif)  
With minimal tweaks, I can change the shader to get this result:
![Version 2 Gif](./files/shaderTest02_ver2.gif)

## Dialogue Test

A dialogue system is a key part of many different types of games. I created one last year, following along with a few different videos. But I wanted to attempt to make one completely on my own. So using Serializable Objects and a lengthy script, I managed to create one. Its fairly easy to set up, as you can create a Dialogue Object, which features the speakers name, the sentences they will speak, and then the available responses. Each response points to a new dialogue object, creating a dialogue chain.

Additional features include 'additional dialogues', which play after the dialogue has ended. This was meant to replicated going back to an NPC after talking to them. These additional dialouges can be applied after a dialogue object, or after responses.

I am very proud of this part of the project specifically because it is a fairly complicated system that I am happy I was able to create without much help from online. The script could certainly do with some refining and potentially splitting into two separate scripts, but I am still incredibly happy with the result.

#### Reuslt
![Dialogue Test Gif](./files/dialogueTest.gif)
