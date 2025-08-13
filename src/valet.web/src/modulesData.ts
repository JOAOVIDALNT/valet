import type { ModuleObj } from "./moduleObj";

export async function fetchModulesData(): Promise<ModuleObj[]> {
    return [
        {
            module: "Introduction",
            title: "Welcome to Valet",
            content: "This module introduces you to the Valet framework."
        },
        {
            module: "Config",
            title: "Configuration",
            content: "Learn how to configure your Valet application."
        },
        {
            module: "Core",
            title: "Core Concepts",
            content: "Understand the core concepts of Valet."
        },
        {
            module: "Auth",
            title: "Authentication",
            content: "Implement authentication in your Valet application."
        }
    ];
}