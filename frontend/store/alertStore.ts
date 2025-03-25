import { create } from "zustand";

interface AlertState {
    isVisible: boolean;
    title: string;
    text: string;
    icon: "success" | "error" | "warning" | "info" | "question";
    showAlert: (title: string, text: string, icon: "success" | "error" | "warning" | "info" | "question") => void;
    hideAlert: () => void;
}

export const useAlertStore = create<AlertState>((set) => ({
    isVisible: false,
    title: "",
    text: "",
    icon: "info",
    showAlert: (title, text, icon) =>
        set({
            isVisible: true,
            title,
            text,
            icon,
        }),
    hideAlert: () =>
        set({
            isVisible: false,
            title: "",
            text: "",
            icon: "info",
        }),
}));
