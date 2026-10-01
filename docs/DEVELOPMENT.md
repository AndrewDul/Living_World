\# Living World RPG — Development Guide



\## Purpose



This document defines the day-to-day development workflow for the Living World project.



The goal is to keep changes safe, understandable, testable, and easy to review.



\---



\# 1. Project location



Current project path:



```text

D:\\Dev\\Living\_World\\Game\_Living\_World\\LivingWorld



Unreal project:

LivingWorld.uproject



Main runtime module:

LivingWorld



2\. Engine

Current engine version:

Unreal Engine 5.8



Installed at:

D:\\Unreal\_Engine\\UE\_5.8



Do not upgrade the engine version without explicit approval.

Do not modify engine source unless explicitly required.

3\. Development philosophy

Work in small, verifiable steps.

Each change should ideally:

1\. solve one clear problem

2\. avoid unrelated refactors

3\. compile successfully

4\. be testable in Unreal

5\. leave the project in a working state

Do not combine multiple major systems into one change unless necessary.

4\. Before changing code

Before implementing a task:

\- inspect relevant existing files

\- identify the owning system

\- check whether similar functionality already exists

\- understand dependencies

\- propose the smallest sensible implementation

Do not create duplicate systems.

Do not replace working code merely because another architecture looks cleaner.

5\. C++ workflow

Core systems should generally be implemented in C++.

Typical workflow:

inspect existing code

↓

design small change

↓

edit C++ files

↓

compile

↓

fix errors

↓

open Unreal if needed

↓

test behavior

↓

review git diff

↓

commit



Prefer changes that compile independently.

6\. Unreal Header Tool awareness

Changes involving Unreal reflection may require a full build.

Examples:

UCLASS()

USTRUCT()

UENUM()

UFUNCTION()

UPROPERTY()



Do not assume Live Coding is sufficient for all structural C++ changes.

If reflected types change significantly, close Unreal Editor and perform a normal build when necessary.

7\. Build configuration

During normal development, prefer:

Development Editor

Win64



Do not treat successful compilation as proof that gameplay behavior is correct.

Compilation and runtime testing are separate validation steps.

8\. Unreal Editor workflow

When testing gameplay changes:

\- open the correct project

\- verify the expected map

\- run Play In Editor

\- test only the feature being changed

\- watch Output Log for warnings/errors

Do not ignore recurring warnings.

Do not modify random editor settings while debugging unrelated problems.

9\. Content folder

Project-owned content should gradually move toward:

Content/LivingWorld/



Current intended high-level structure includes:

Content/LivingWorld/

&#x20;   Characters/

&#x20;   Core/

&#x20;   Data/

&#x20;   Items/

&#x20;   Maps/

&#x20;   NPC/

&#x20;   UI/

&#x20;   World/



Do not move or delete Unreal template assets casually.

Asset relocation can create redirectors and broken references.

Content cleanup should be a separate intentional task.

10\. Source structure

The main source module is:

Source/LivingWorld/



Expected logical areas may include:

Player/

Interaction/

Time/

WorldState/

AI/

NPC/

Inventory/

Quests/

Save/

Data/

UI/



Do not create all folders merely for appearance.

Create them when real code belongs there.

11\. Naming

Use Unreal naming conventions.

Examples:

ALivingWorldCharacter

ULivingWorldTimeSubsystem

FLivingWorldDateTime

ELivingWorldSeason

ILivingWorldInteractable

ULivingWorldInventoryComponent



Prefer names that clearly describe responsibility.

Avoid vague names such as:

Manager

Helper

Utility

System2

Thing

DataObject



unless the name genuinely reflects the role.

12\. Components

Use Actor Components when behavior naturally belongs to an Actor but should remain reusable.

Examples:

\- inventory

\- interaction detection

\- NPC runtime state bridge

\- health

\- equipment

Do not put every feature directly into the player character class.

13\. Subsystems

Use Unreal Subsystems only when lifetime and access requirements justify them.

Possible candidates:

\- game time

\- world-state service

\- save service

Do not use a subsystem simply to avoid passing references.

Global accessibility should not replace clear ownership.

14\. Blueprint policy

Blueprints are allowed and expected.

Use them for:

\- visual setup

\- content assembly

\- asset references

\- animation integration

\- UI

\- simple configuration

Avoid large Blueprint graphs containing core persistent simulation rules.

If a Blueprint graph becomes difficult to reason about, consider moving reusable logic into C++.

15\. Data Assets and Data Tables

Prefer data-driven content for game entities.

Examples:

NPC definitions

item definitions

schedule definitions

location definitions

rumor definitions



Do not hardcode large content datasets inside C++ source files.

16\. Logging

Use Unreal logging instead of temporary uncontrolled console output.

Create meaningful log categories when needed.

Example:

UE\_LOG(LogTemp, Log, TEXT("Current world hour: %d"), Hour);



LogTemp is acceptable during early experiments.

Long-term systems should use dedicated log categories.

Do not spam logs every frame.

17\. Assertions and validation

Use Unreal validation tools appropriately.

Examples:

check()

ensure()

IsValid()



Do not use assertions for normal gameplay conditions that can legitimately fail.

Validate external/data-driven input defensively.

18\. Performance rules

Avoid unnecessary work every frame.

Before using Tick, ask whether the logic can use:

\- timer

\- delegate

\- time event

\- state transition

\- scheduled update

For large systems, prefer batched or distributed updates rather than updating everything simultaneously.

19\. Save compatibility

Persistent data structures should be designed with future versioning in mind.

Do not assume save files will always match the current C++ struct layout.

Save-system changes should eventually include version identifiers and migration strategies where needed.

Prototype 0.1 only needs a simple foundation, not a complete migration framework.

20\. Git workflow

The main branch is:

main



Remote:

origin



Repository:

https://github.com/AndrewDul/Living\_World.git



Before starting significant work:

git status



After making changes:

git status

git diff



When ready:

git add .

git commit -m "Clear description of change"

git push



Commits should describe what changed.

Good:

Add base world time subsystem

Add interaction interface

Implement prototype NPC schedule data



Bad:

stuff

changes

update

fix

aaa



21\. Git LFS

Git LFS is enabled.

The following Unreal binary assets are tracked through LFS:

\*.uasset

\*.umap



Do not remove these rules without explicit reason.

22\. Ignored Unreal folders

Do not commit generated folders:

Binaries/

DerivedDataCache/

Intermediate/

Saved/



These can be regenerated locally.

23\. Reviewing changes

Before committing, inspect:

git status



For code/text changes also inspect:

git diff



Be suspicious if unrelated files changed unexpectedly.

Do not commit accidental editor-generated changes without understanding them.

24\. Debugging rule

When something breaks:

1\. stop adding new features

2\. reproduce the problem

3\. read the actual error

4\. isolate the failing system

5\. fix the smallest root cause

6\. rebuild

7\. retest

Do not stack speculative fixes.

25\. Claude workflow

Claude may assist with coding, architecture, debugging, and refactoring.

Before editing code, Claude should read:

CLAUDE.md

docs/ARCHITECTURE.md

docs/DEVELOPMENT.md



Claude should inspect the relevant repository files before proposing implementation.

Claude must not assume that planned systems already exist.

Claude should distinguish between:

existing implementation

planned architecture

future concept



26\. Claude change policy

Claude should NOT:

\- rewrite large parts of the project without approval

\- delete template content without approval

\- install plugins without approval

\- modify engine files

\- change Unreal Engine version

\- force-push Git history

\- add large speculative frameworks

\- create systems unrelated to the active task

Claude should:

\- explain the proposed change

\- keep scope narrow

\- modify the minimum necessary files

\- compile or provide build instructions

\- report changed files

\- mention any assumptions

27\. Prototype quality level

Prototype 0.1 does not need production-quality art.

It does need:

\- stable code

\- understandable architecture

\- persistence foundation

\- reproducible behavior

\- clean system boundaries

Placeholder visuals are acceptable.

Broken architecture hidden behind good visuals is not.

28\. Current development priority

The current priority is to build the technical vertical slice.

Do not build the full open world yet.

Do not implement large political, economic, warfare, dynasty, or supernatural systems yet.

Those systems belong later.

Current success means proving that a small area can behave like a living persistent world.

29\. Definition of done for a development task

A task is considered complete when:

\- requested behavior exists

\- project compiles

\- obvious errors are resolved

\- feature can be tested

\- unrelated systems were not broken

\- changed files are known

\- Git status is understood

30\. Guiding rule

Prefer the simplest implementation that supports the current prototype without creating an obvious architectural dead end.

