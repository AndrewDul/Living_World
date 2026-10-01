\# Living World RPG — Architecture



\## Purpose



This document describes the intended technical architecture of the Living World project.



The project is an Unreal Engine 5.8 C++ open-world sandbox RPG with a persistent simulated world.



The architecture should support a small prototype first while remaining suitable for future expansion.



\---



\# 1. Main principles



The architecture follows these rules:



\- C++ contains core gameplay and simulation logic.

\- Blueprints remain thin.

\- Systems are data-driven.

\- Persistent entities use stable IDs.

\- The world has one authoritative game-time source.

\- NPC simulation supports multiple levels of detail.

\- Save/load is designed early, not added at the end.

\- Systems should be modular but remain inside one Runtime module during the prototype.

\- Avoid speculative abstractions that are not needed yet.



\---



\# 2. Main Unreal module



For Prototype 0.1 the project uses one Runtime module:



`LivingWorld`



Do not create additional Runtime modules unless there is a concrete architectural reason.



Possible future modules may exist later, but they are not required now.



\---



\# 3. Source structure



Expected C++ structure:



```text

Source/

└── LivingWorld/

&#x20;   ├── AI/

&#x20;   ├── Data/

&#x20;   ├── Interaction/

&#x20;   ├── Inventory/

&#x20;   ├── NPC/

&#x20;   ├── Player/

&#x20;   ├── Quests/

&#x20;   ├── Save/

&#x20;   ├── Time/

&#x20;   ├── UI/

&#x20;   ├── WorldState/

&#x20;   ├── LivingWorld.cpp

&#x20;   ├── LivingWorld.h

&#x20;   └── LivingWorld.Build.cs

Folders may be added gradually.

Do not create empty systems just to match this structure.

4\. World Time

The game must have one authoritative world-time system.

Responsibilities:

\- current year

\- month

\- day

\- hour

\- minute

\- season

\- time progression

\- time skipping

\- notifications when meaningful time boundaries are crossed

Examples:

\- new minute

\- new hour

\- new day

\- new month

\- season change

Other systems should query or subscribe to World Time rather than maintaining separate clocks.

The system must not depend on the real-world system clock.

No offline progression.

5\. World State

World State represents persistent facts about the simulated world.

Examples:

\- current world date

\- NPC state

\- NPC deaths

\- ownership changes

\- important object states

\- relationships

\- active world events

\- settlement state

\- persistent interaction state

World State should be separated from temporary runtime actor state.

Actors may be destroyed or unloaded while their persistent state remains known.

6\. Persistent identity

Important persistent entities need stable identifiers.

A persistent ID should not depend on:

\- actor pointer

\- actor memory address

\- transient runtime name

\- spawn order

Candidates for persistent identity include:

\- NPC

\- settlement

\- location

\- important item

\- faction

\- persistent object

\- world event

Stable IDs will be required for save/load and distant simulation.

7\. NPC architecture

An important NPC should conceptually have two layers.

Definition

Static or mostly static data:

\- persistent ID

\- name

\- sex

\- age or birth date

\- home

\- profession

\- family

\- faction

\- base schedule

\- personality-related data

\- known relationships

Runtime / persistent state

Changing data:

\- current location

\- current activity

\- health

\- inventory

\- relationships

\- knowledge

\- rumors

\- current schedule state

\- alive/dead

\- temporary conditions

NPC data should not be hardcoded directly into individual C++ classes.

8\. NPC simulation LOD

NPC simulation must support several levels of detail.

Level A — Active

Near the player.

May include:

\- Character Actor

\- AI Controller

\- navigation

\- animation

\- perception

\- interaction

\- detailed schedule behavior

Level B — Simplified

Farther from the player.

May simulate:

\- location

\- activity

\- schedule progression

\- important events

without full physical Actor simulation.

Level C — Abstract

Outside active areas.

NPC exists primarily as data.

Simulation may process:

\- travel completion

\- work

\- sleep

\- relationship events

\- important life events

\- world-event consequences

The exact implementation can evolve later.

Prototype 0.1 only needs the foundation for this distinction.

9\. NPC knowledge

Objective world facts and NPC knowledge are separate concepts.

An NPC may believe something that is:

\- correct

\- incomplete

\- outdated

\- exaggerated

\- false

\- deliberately fabricated

Do not store every rumor as a global truth flag.

The architecture should eventually allow information to have:

\- source

\- subject

\- content

\- timestamp

\- confidence or certainty

\- distortion

\- ownership by individual NPCs

Prototype 0.1 only needs a simple version.

10\. Interaction system

Player interaction should use a reusable interface or component-based pattern.

Possible interactables:

\- doors

\- beds

\- containers

\- NPCs

\- farm objects

\- tavern objects

\- pickups

The player should not contain special-case code such as:

if object is bed

if object is door

if object is cow

if object is NPC



Prefer a generic interaction contract.

Interaction should eventually support:

\- display name

\- interaction prompt

\- validation

\- execution

\- optional state

\- persistence where necessary

11\. Inventory and items

Items should be data-driven.

An item definition may contain:

\- stable item ID

\- display name

\- type

\- weight

\- value

\- description

\- icon

\- stack size

\- gameplay tags or categories

Runtime inventory should reference item definitions instead of duplicating static data.

Prototype 0.1 only requires a simple inventory.

12\. Save system

Save/load must preserve world state rather than attempting to serialize the entire Unreal level.

The save should eventually contain data such as:

SaveGame

├── SaveVersion

├── PlayerState

├── WorldTime

├── NPCStates

├── PersistentObjectStates

├── RelationshipStates

└── WorldEventStates



Do not save raw actor pointers.

Do not assume every Actor exists when loading.

Save data should support future versioning.

13\. Player

The player character should focus on player-specific concerns.

Examples:

\- movement

\- camera

\- interaction initiation

\- controlled-character state

Avoid turning the player class into a global container for:

\- world time

\- NPC simulation

\- quests

\- economy

\- saving

\- global world state

Those belong to separate systems.

14\. GameInstance and Subsystems

Prefer Unreal subsystem patterns where lifetime semantics fit.

Potential uses:

\- GameInstanceSubsystem for systems that should survive map transitions

\- WorldSubsystem for systems tied to a loaded World

Do not automatically make every system a subsystem.

Choose lifetime intentionally.

15\. Actor Components

Use Actor Components for reusable behavior owned by Actors.

Possible future examples:

\- interaction component

\- inventory component

\- knowledge component

\- relationship component

Do not create components for behavior that has no meaningful Actor ownership.

16\. Data Assets and Data Tables

Prefer Unreal data systems for designer-editable content.

Possible usage:

Data Assets

Good for:

\- item definitions

\- NPC archetypes

\- professions

\- interaction definitions

\- world configuration

Data Tables

Good for:

\- structured lists

\- schedules

\- item sets

\- simple balancing data

JSON

May be useful for:

\- external simulation data

\- generated content

\- bulk imports

\- AI-assisted pipelines

Do not choose JSON merely because it is easy to generate.

17\. Event communication

Prefer event-driven communication when practical.

Examples:

World Time emits:

OnHourChanged

OnDayChanged



NPC schedule system can react without being directly controlled by the time system.

Avoid tightly coupling unrelated systems.

Avoid global event systems that become impossible to trace.

18\. Tick policy

Tick should not be the default solution.

Before adding Tick, consider:

\- timers

\- delegates

\- scheduled updates

\- event callbacks

\- batch simulation

\- lower-frequency updates

Especially avoid per-frame Tick on large numbers of NPCs.

19\. Prototype 0.1 dependency direction

Initial dependency direction should roughly be:

World Time

&#x20;   ↓

NPC Schedule

&#x20;   ↓

NPC Activity



World State

&#x20;   ↑

Save System



Item Definitions

&#x20;   ↓

Inventory



Interaction Interface

&#x20;   ↓

Player Interaction



NPC Knowledge

&#x20;   ↓

Dialogue / Rumor



Systems should depend on clear abstractions rather than directly manipulating unrelated systems.

20\. Prototype 0.1 implementation order

Recommended technical order:

1\. project foundation

2\. interaction framework

3\. world time/calendar

4\. day/night presentation

5\. simple NPC base

6\. NPC schedule

7\. simple dialogue

8\. knowledge/rumor prototype

9\. item definition

10\. simple inventory

11\. farm interaction

12\. bed/sleep interaction

13\. world-state persistence

14\. save/load integration

Do not build the full continent before these foundations work.

21\. First playable slice

Prototype 0.1 should prove a small living-world loop.

Example:

Player wakes at home

↓

Time progresses

↓

Family NPCs follow schedules

↓

Player performs a farm interaction

↓

Player visits tavern

↓

NPC shares information/rumor

↓

Evening arrives

↓

Player sleeps

↓

Next day begins

↓

Save

↓

Reload

↓

World state remains consistent



If this loop works reliably, the architecture has proven its basic direction.

22\. Things explicitly deferred

Do not implement yet:

\- full continent

\- large-scale economy

\- political simulation

\- dynamic wars

\- thousands of NPCs

\- procedural quest generation

\- advanced combat

\- Second Side supernatural systems

\- settlement construction

\- multiplayer

\- dynasty system

\- full aging simulation

\- large AI language-model NPC system

The architecture may leave room for them, but Prototype 0.1 must not depend on them.

23\. Main rule

Build the smallest correct foundation that can grow later.

Do not attempt to solve the entire future game before the first playable living-world loop exists.

