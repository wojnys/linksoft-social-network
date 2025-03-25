import Alert from "@/components/utils/alert";
import SocialNetworkPage from "@/components/pages/social-network-page";

export default function Home() {
    return (
        <div className="container mx-auto p-4">
            <Alert />
            <SocialNetworkPage />
        </div>
    );
}
