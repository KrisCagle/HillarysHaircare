---
description: Generate an ordered implementation plan from a PRD. Breaks the project into steps with checkboxes that pair-program uses to track progress.
argument-hint: (no arguments)
allowed-tools: Read, Write, Edit, Bash, Glob, Grep
---

# Create Roadmap

You are helping a developer break their project into an ordered implementation plan. Each step becomes a unit of work for `/pair-program`.

## Prerequisites

A PRD must exist. Check `tandem.json` for a doc tagged `prd`. If none exists, tell the developer:

> "I need a PRD to create a roadmap from. Want to run `/create-prd` first?"

## Workflow

### Step 1: Read Context

1. **Read the PRD** (found via manifest, tagged `prd`).
2. **Read the architecture doc** if one exists (tagged `architecture`). This informs the order and scope of steps.
3. **Read `~/.claude/CLAUDE.md`** if it has a Developer Profile. The skill tiers (Currently Learning, Deepening, Strong Skills) may lightly inform step ordering: put foundational setup before topics the developer is actively learning, so they have context before hitting unfamiliar territory.

### Step 2: Break Into Steps

Decompose the project into ordered implementation steps. Each step should be:

- **Ordered by dependency:** What has to exist before what.
- **Completable in one session:** Roughly 1-4 hours of work per step.
- **Focused:** Each step has a clear deliverable.

Include acceptance criteria for each step where applicable, following the **Given/When/Then** format:

- **Given** [a starting context or precondition]
- **When** [an action occurs]
- **Then** [the expected outcome]

Each step should have one or more acceptance criteria that define what "done" looks like. Not every step needs them: pure infrastructure setup or refactoring may not map cleanly to behavioral criteria. But anything that delivers a user-facing or testable outcome should have at least one. These criteria ground the work in concrete, verifiable behavior and feed directly into `/create-issues` if the developer converts steps to GitHub issues later.

### Step 3: Write the Roadmap

Default location: `dev-docs/ROADMAP.md`, or wherever the developer specifies.

```markdown
<!-- Last updated: [date] -->
<!-- Last change: Initial roadmap creation -->

# [Project Name] - Implementation Roadmap

Generated from: [PRD path from tandem.json]

## Steps

- [ ] **Step 1: [Title]**
  [Brief description of what this step covers and what the deliverable is]
  [Reference to architecture component if applicable]

  **Acceptance Criteria** (where applicable):
  - **Given** [precondition], **When** [action], **Then** [expected outcome].
  - **Given** [precondition], **When** [action], **Then** [expected outcome].

- [ ] **Step 2: [Title]**
  [Brief description]

...
```

### Step 4: Register in Manifest

Update `tandem.json`:

```json
{
  "path": "dev-docs/ROADMAP.md",
  "scope": "general",
  "purpose": "Ordered implementation steps with checkboxes and acceptance criteria",
  "tags": ["roadmap", "planning"]
}
```

### Step 5: Review with Developer

Walk through the roadmap. Get feedback on ordering, scope, and step size. Revise as needed.

## Design Notes

- Keep steps minimal. No time estimates or complexity scores.
- The checkbox format (`- [ ]` / `- [x]`) is what `/pair-program` uses to mark steps complete.
- Steps can reference specific architecture components when it adds clarity, but this isn't required. `/pair-program` reads both documents regardless.
- Never use em dashes in any written content. Use commas, periods, colons, or semicolons instead.
