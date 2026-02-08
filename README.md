# BookLibrary TDD Exercise
Implement Author and Book to Satisfy Tests

**Goal**

You will implement twodomain classes—Author and Book—using **Test-Driven Development**. Your implementation must satisfy **all**the provided xUnit tests in BookLibrary.Test.

**Focus:** Model class **invariants**and ensure **bidirectional consistency** between Author and Book.

**Acceptance Criteria(Derived from Tests)**

**Author**

*   FirstName and LastName are **required**, trimmed, and **non-empty**.
    

*   Optional BirthYear and DeathYear:
    

*   Maintains a collection Books:
    

*   Books must be **read-only** from outside (no public setter).
    

*   ToString() returns "FirstName LastName" (trimmed).
    

**Book**

*   Required:
    

*   Setting/Changing operations must re-check invariants:
    

*   Creating a Book must **register it with the Author** (bidirectional link).
    

*   Changing Author must **remove** it from the previous author and **add** it to the new author.
    

*   Isbn is optional; store null if blank. No format validation required.
    

*   ToString() returns "Title (Year) by FirstName LastName".
    

** TDD Workflow (What to Do)**

2.  **Run the tests**: dotnet test\\ Observe failures (red).
    

4.  **Pick one failing test at a time** (e.g., Author\_Constructs\_With\_Minimum\_Valid\_Data) and implement the **minimal** change to make it pass (green).
    

6.  **Refactor safely**, keeping all tests green.
    

8.  Repeat until **all tests pass**.
    

Tip: Implement basicconstruction first, then invariants, then mutations (ChangeTitle, ChangeAuthor, etc.), then edge cases.
