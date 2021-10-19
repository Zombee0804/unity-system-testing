# unity-system-testing

## Table Of Contents
* [Overview](#overview)
* [Platformer](#platformer-test)
* [Shader Testing](#shader-test)

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

## Shader Test
I have never really though about using shaders in Unity before. In a recent venture into GameMaker Studio 2, I began to look into shaders a bit more (although never ended up using them). Shaders in GameMaker have their own scripting language, and I was expecting a similar thing in Unity. However, shaders in Unity are created in a shader graph, which is a lot easier to use than I originally thought. I created some basic gameplay and added a red flash when the player is hit, and a dissolve effect (ripped straight from a [Brackeys video](https://youtu.be/5dzGj9k8Qy8)) when the player dies.

I did go a tad overboard with the shaders, and the gameplay and collisions are far from perfect. But the focus was learning how to setup up and create basic shaders, so I wasn't too worried about perfection.

#### Result
![Shader Test Gif](./files/shaderTest.gif)
