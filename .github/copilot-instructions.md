# Introduction and Conventions

The project refers to a development for managing the preparation of the Brescia-style skewer ("spiedo bresciano").

# Instructions for Copilot

The goal is to ensure that the produced code meets the requirements, is professional, and of high quality.  
If you encounter ambiguity or lack of details, ask for clarification before proceeding.

If I am asking you to explain something or posing a question, evaluate not making immediate changes. Instead, answer the questions and clarify any doubts.

When applying a modification, avoid explaining every step — keep the response concise. However, when answering a question, include details, references, and explanations.

## Modifying Existing Files

If you modify an existing file, stick to what you are implementing. For example:  
- If you are adding a method, avoid refactoring unrelated parts of the file.  
- If you are implementing logic, avoid reformatting other code sections.  

Work within the context of a specific change, as if it were the logical content of a commit, with coherent modifications.  
If you believe additional information or changes are relevant, do **not** include them directly in the code — highlight them as suggestions or questions.

- Preserve existing `import` and `using` statements unless explicitly instructed to remove them.  
- Preserve other logic unless it is explicitly part of the modification.  
- If asked to modify `appsettings.json` or configuration files to add data, keep all existing data and simply add the requested items.  
- If asked to extract a class, method, or logic, replace the original code with the extracted version and use it wherever reusable.  

## Code Writing Guidelines

Follow a professional approach, adhering to standards like an expert software engineer:

1. Write clear, readable, and well-documented code.  
2. Follow the coding conventions of the language used.  
3. Avoid introducing unnecessary complexity.  
4. Add comments only if necessary to explain implementation choices.  
5. Maintain a modular and reusable approach wherever applicable.  
6. Code and comments in the code must be in **English**, even if instructions are in Italian.  

### Comments

Comments should be avoided, especially if they repeat what the code already does. Use comments only to explain implementation decisions or complex logic.  

**Example of a comment to avoid:**  
```csharp
salesOrder.Id = Guid.NewGuid(); // generate a new Guid
