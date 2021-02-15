# MidiStreamingBlazorApp
This repo demonstrates the functionality of streaming MIDI events from an electronic piano keyboard to a Blazor web service which displays the notes being played real time. 

## Problem Being Solved
The goal of the project is to provide a solution to the challenges of distance learning piano lessons. It is complicated for both teachers and students to complete piano lessons over the internet for a number of reasons, including the fact that both parties are not able to see the notes being played visually.

This application helps students and teachers to perform piano lessons by providing a shared virtual piano between them, along with allowing notes to be streamed from a MIDI capable keyboard.

This solution is composed of two parts: a console application and a web based Blazor Server app

### Console App

The console app is to be run on a computer or device running Windows 10 that is connected to a MIDI keyboard. All notes played on the keyboard are streamed to the web application.

### Web Application

This application allows users to create "Meetings" and share a meeting link with others in order to collaborate with a shared virtual keyboard. As users running the console application above play notes, their key presses will appear for every member in the meeting to see.
