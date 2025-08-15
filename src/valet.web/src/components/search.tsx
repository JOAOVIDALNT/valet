import { useState } from "react";
import type { ModuleObj } from "../moduleObj"
import { fetchModulesData } from "../modulesData";

type SearchProps = {
    onSelectModule: (module: ModuleObj, term?: string) => void
}

export default function Search({onSelectModule} : SearchProps) {
    const [query, setQuery] = useState("");
    const [results, setResults] = useState<ModuleObj[]>([]);

    async function handleSearch(e: React.ChangeEvent<HTMLInputElement>) {
        const value = e.target.value;
        setQuery(value);
        if (value.trim() === "") {
            setResults([]);
            return;
        }
        const modules = await fetchModulesData();

        
        const filtered = modules.filter(key =>
            key.content.toLowerCase().includes(value.toLowerCase())
        )
        setResults(filtered);
    }

    function handleClick(module: ModuleObj) {
        onSelectModule(module, query);
        setQuery("");
        setResults([]);
    }

    function getSnippet(content: string, query: string, snippetLength = 60) {
    const index = content.toLowerCase().indexOf(query.toLowerCase());
    if (index === -1) return "";
    const start = Math.max(0, index - snippetLength / 2);
    const end = Math.min(content.length, index + query.length + snippetLength / 2);
    let snippet = content.substring(start, end);
    // Opcional: destaca o termo buscado
    const regex = new RegExp(`(${query})`, "gi");
    snippet = snippet.replace(regex, "<mark>$1</mark>");
    return snippet;
}

    return (
        <div className="text-white px-8 py-4 bg-neutral-800 mb-6 rounded-lg w-80 center mx-auto mt-4 ">
            <input type="text" placeholder="search" className="outline-none w-full text-2xl"
            value={query}
            onChange={handleSearch}/>
            {results.length > 0 && (
                <ul className="fixed mt-4 bg-neutral-900 rounded-lg w-70">
                    {results.map((item: ModuleObj) => (
                        <li
                            key={item.title}
                            className="cursor-pointer px-4 py-2 hover:bg-neutral-600"
                            onClick={() => handleClick(item)}
                        >
                            <strong>{item.title}</strong>
                            <br />
                            <span
                            dangerouslySetInnerHTML={{
                            __html: getSnippet(item.content, query)
                            }}
                        />
                        </li>
                    ))}
                </ul>
            )}
        </div>
    )
}