import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { ConfigProvider } from 'antd'
import viVN from 'antd/locale/vi_VN'
import dayjs from 'dayjs'
import 'dayjs/locale/vi'
import './index.css'
import App from './App.tsx'

// Set Vietnamese locale for dayjs
dayjs.locale('vi')

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ConfigProvider 
      locale={viVN}
      theme={{
        token: {
          colorPrimary: '#1890ff',
          borderRadius: 8,
          fontFamily: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial',
        },
        components: {
          Layout: {
            headerBg: '#ffffff',
            siderBg: '#001529',
          },
        },
      }}
    >
      <App />
    </ConfigProvider>
  </StrictMode>,
)
