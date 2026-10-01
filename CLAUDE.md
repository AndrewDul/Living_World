\# Living World RPG — Claude Instructions



\## Project



Living World is an open-world sandbox RPG built in Unreal Engine 5.8.



The game focuses on a persistent, autonomous medieval-inspired world that continues to function independently of the player.



The player begins as an ordinary person in a rural family environment and may remain local or eventually influence trade, politics, war, settlements, factions, and the wider world.



The project is currently in early prototype development.



\---



\## Engine and platform



\- Unreal Engine 5.8

\- Windows PC is the primary development platform

\- Main project module: `LivingWorld`

\- Project type: C++ Unreal project

\- World Partition will be used for the game world

\- Multiplayer is NOT currently implemented

\- Architecture should avoid unnecessary decisions that would make future small-scale co-op impossible



\---



\## Core technical philosophy



\### C++ first



Core gameplay and simulation systems should be implemented primarily in C++.



Use C++ for:



\- world time

\- calendar

\- world state

\- persistence

\- NPC simulation

\- interaction systems

\- inventory logic

\- knowledge and rumor systems

\- save/load

\- AI-related gameplay logic

\- simulation systems

\- reusable gameplay frameworks



Blueprints should be thin and used mainly for:



\- presentation

\- asset references

\- animation integration

\- level scripting when appropriate

\- simple content configuration

\- UI wiring

\- designer-facing configuration



Do not move core simulation logic into large Blueprint graphs.



\---



\## Data-driven design



Avoid hardcoding individual NPCs, items, schedules, quests, or world data directly in gameplay code.



Prefer:



\- Data Assets

\- Data Tables

\- structs

\- configuration files

\- JSON where appropriate

\- stable IDs



Gameplay systems should operate on data rather than special-case individual content.



\---



\## Persistent world



The world must be designed around persistence from the beginning.



Important entities should have stable identifiers.



Examples:



\- NPCs

\- locations

\- settlements

\- important items

\- factions

\- world events

\- interactable persistent objects



The save system will eventually need to preserve state such as:



\- world date and time

\- NPC state

\- NPC locations

\- relationships

\- deaths

\- inventories

\- ownership

\- world events

\- player state

\- important object state



Do not rely only on actor names or runtime-generated pointers for persistent identity.



\---



\## World simulation



Do NOT assume every NPC runs full detailed simulation at all times.



The intended model is simulation LOD:



1\. Near the player:

&#x20;  - detailed AI

&#x20;  - movement

&#x20;  - perception

&#x20;  - interaction

&#x20;  - schedules



2\. Farther away:

&#x20;  - simplified simulation



3\. Outside active areas:

&#x20;  - event/state-based simulation



Architecture should allow simulation detail to scale with relevance and distance.



Do not design systems that require thousands of fully ticking actors.



\---



\## NPC design direction



Important NPCs will eventually contain data such as:



\- persistent ID

\- name

\- age

\- home

\- family

\- profession

\- faction

\- relationships

\- schedule

\- knowledge

\- beliefs

\- rumors

\- current state



NPC knowledge is NOT the same as objective world truth.



NPC information may be:



\- correct

\- incomplete

\- outdated

\- exaggerated

\- misunderstood

\- deliberately false



Systems should preserve this distinction.



\---



\## Time system



The game uses a continuous fictional world calendar.



World systems should share one authoritative game-time source.



Time will eventually support:



\- hours

\- days

\- months

\- years

\- seasons

\- scheduled NPC behavior

\- economy

\- events

\- travel

\- weather

\- agriculture

\- politics



Do not tie world time directly to the real-world clock.



Do not implement offline world aging.



\---



\## Project structure



Keep systems separated by responsibility.



Expected areas include:



\- Player

\- Interaction

\- Time

\- WorldState

\- AI

\- NPC

\- Inventory

\- Quests

\- Save

\- Data

\- UI



Avoid unnecessary modules during the prototype phase.



Start with the existing main Runtime module unless there is a strong reason to split it.



\---



\## Coding rules



Follow Unreal Engine C++ conventions.



Prefer:



\- clear names

\- small focused classes

\- composition over giant god classes

\- Unreal reflection macros only where needed

\- const correctness

\- explicit ownership

\- weak references where appropriate

\- comments explaining WHY, not obvious WHAT



Avoid:



\- giant managers containing unrelated systems

\- hidden global mutable state

\- excessive Tick usage

\- circular dependencies

\- hardcoded asset paths where avoidable

\- unnecessary singletons

\- unnecessary plugins

\- speculative abstractions with no current use



\---



\## Performance



The development machine currently has an RTX 3050 Ti and 64 GB RAM.



Performance should be considered early.



Avoid architectures that assume unlimited CPU/GPU resources.



Especially avoid:



\- thousands of actors ticking every frame

\- expensive global searches every frame

\- full AI simulation for distant NPCs

\- unnecessary Blueprint Tick logic

\- unnecessary dynamic allocations in hot loops



\---



\## Prototype 0.1



The first playable prototype should focus on:



1\. player start

2\. waking at home

3\. movement and camera

4\. interaction framework

5\. world time/calendar

6\. day/night

7\. 3–5 NPCs

8\. NPC schedules

9\. simple dialogue

10\. NPC knowledge/rumor

11\. simple inventory/item

12\. one farm interaction

13\. family tavern

14\. sleeping

15\. advancing to the next day

16\. save/load persistent world state



The first playable area is intentionally small:



\- family home

\- farm

\- uncle's tavern

\- small part of the village

\- fields

\- forest

\- nearby water

\- road leaving the area



Do NOT start building the full continent during Prototype 0.1.



\---



\## Workflow



Before making significant changes:



1\. inspect the existing code

2\. explain the proposed implementation

3\. keep the change scoped to the requested task

4\. avoid unrelated refactors

5\. build or validate when possible

6\. report exactly what was changed



Do not silently redesign existing architecture.



Do not delete template content unless explicitly requested.



Do not introduce third-party plugins without approval.



Do not implement future systems merely because they may eventually be useful.



\---



\## Git



Keep commits focused and understandable.



Do not commit generated Unreal folders such as:



\- `Binaries`

\- `Intermediate`

\- `Saved`

\- `DerivedDataCache`



Unreal binary assets such as `.uasset` and `.umap` are handled with Git LFS.



Never rewrite Git history or force-push unless explicitly instructed.



\---



\## Current priority



The current goal is not to build the entire RPG.



The goal is to build a stable technical foundation and one small vertical slice proving that:



\- time passes

\- NPCs live according to schedules

\- the player can interact with the environment

\- information can exist as NPC knowledge/rumors

\- world state persists across save/load



Prefer working software over speculative complexity.

