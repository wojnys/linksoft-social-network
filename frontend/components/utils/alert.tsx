"use client";
import { useAlertStore } from "@/store/alertStore";
import Swal from "sweetalert2";
import { useEffect } from "react";

const Alert = () => {
    const { isVisible, title, text, icon, hideAlert } = useAlertStore();

    useEffect(() => {
        if (isVisible) {
            Swal.fire({
                title,
                text,
                icon,
                allowOutsideClick: true,
                didClose: () => {
                    hideAlert();
                },
            });
        }
    }, [isVisible, title, text, icon, hideAlert]);

    return null;
};

export default Alert;
