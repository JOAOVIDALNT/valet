
import { useEffect } from "react";
import type { ModuleObj } from "../moduleObj";

type ModuleProps = {
    module: ModuleObj | null,
    searchTerm?: string
}

export default function Module({module, searchTerm}: ModuleProps) {

    function getHighlightedContent(content: string, term?: string) {
        if (!term) return content;  
        const regex = new RegExp(`(${term})`, "gi");
        return content.replace(regex, '<mark id="search-mark">$1</mark>');
    }

    useEffect(() => {
        const ref = document.getElementById("search-mark");

        if (ref) {
            ref.scrollIntoView({ behavior: "smooth", block: "center" });
        }
    }, [module, searchTerm]);

    return (
        <main className="text-white flex-1 bg-black p-6 w-full">
            <h1>{module?.title}</h1>
            <p
                className="break-words whitespace-pre-line"
                dangerouslySetInnerHTML={{
                    __html: getHighlightedContent(module?.content || "", searchTerm)
                }}
            />
        </main>
    )
}