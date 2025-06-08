# ITIS Control Work Sem 2

### General Problem Statement

Implement a system for dynamically loading plugins in the form of classes contained in assemblies. Each plugin may depend on other plugins, as specified in the assembly, adjust the order and strategy of required executions based on the dependency graph requirements, and handle loading and potential cycles during initialization.

### Functional Requirements

1. **Dependency Model**
   * Plugin classes may depend on other plugins, indicated via a specific attribute. A single plugin may have multiple dependencies.

2. **Graph Construction**
   * Based on reflective analysis of types from the specified assembly, it is necessary to:
     * Build a directed dependency graph between plugins;
     * Validate the graph for correctness (in particular, ensure there are no cycles).

3. **Topological Sorting and Loading Planning**
   * Perform topological sorting of the plugin graph;
   * Create a plan for parallel loading, where plugins without mutual dependencies can be loaded simultaneously;
   * Plugins must be initialized strictly after all their dependencies.

4. **Parallel Loading with Dependency Control**
   * Implement parallel plugin loading in accordance with the constructed plan;
   * Ensure safe execution with respect to dependencies (no plugin can start loading until all its dependencies have completed loading);
   * Handle exceptions during plugin initialization.

5. **Profiling and Reporting**
   * After loading is complete, it is required to:
     * Output a complete loading chronology: which plugin started and finished loading at what time;
     * Indicate which plugins were loaded in parallel;
     * Record the total loading time (using `Stopwatch` -> `Start()`, `Stop()`, `Elapsed`);
     * List plugins that failed to load, along with the reason for the failure.
    
### Plugins structer

![alt text](https://github.com/timofejr/ITISControlWorkSem2/blob/master/ConsoleApp1/images/plugins_structer.jpg "Logo Title Text 1")
