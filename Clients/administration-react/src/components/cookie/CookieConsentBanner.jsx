import CookieConsent from 'react-cookie-consent';
import styles from './CookieConsentBanner.module.css'

const CookieConsentBanner = () => {

  return (
    <CookieConsent
      contentClasses={styles['cookies__text']}
      containerClasses={styles['cookies__container']}
      buttonStyle={{
        height: "3rem",
        borderRadius: "10px",
        background: "green",
        color: "white",
        fontWeight: "bolder",
      }}
      overlay
    >
      This website uses cookies but only for authentication.
    </CookieConsent>
  );
};

export default CookieConsentBanner;