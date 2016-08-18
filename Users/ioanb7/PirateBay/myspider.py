import scrapy
import urllib

class BlogSpider(scrapy.Spider):
    name = 'blogspider'
    start_urls = ['']
    custom_settings = {"DOWNLOAD_TIMEOUT":'2000'}
    
    def __init__(self, query='robot'):
        self.start_urls = ['https://pirateproxy.red/search/' + urllib.quote(query) + '/0/99/0']
    
    def parse(self, response):
        titles = []
        urls = []
        
        for url in response.css('#searchResult tr a.detLink::text'):
            titles.append(url.extract())
        for url2 in response.xpath('//a[contains(@title, "Download this torrent using magnet")]//@href'):
            urls.append(url2.extract())
        
        #[yield {"Title": titles[x], "Url": urls[x]} for x in xrange(0, len(titles)) ]
        for x in xrange(0, len(titles)):
            yield {"Title": titles[x], "Url": urls[x]}

    def errback_httpbin(self, failure):
        # log all failures
        self.logger.error(repr(failure))

        # in case you want to do something special for some errors,
        # you may need the failure's type:

        if failure.check(HttpError):
            # these exceptions come from HttpError spider middleware
            # you can get the non-200 response
            response = failure.value.response
            self.logger.error('HttpError on %s', response.url)

        elif failure.check(DNSLookupError):
            # this is the original request
            request = failure.request
            self.logger.error('DNSLookupError on %s', request.url)

        elif failure.check(TimeoutError, TCPTimedOutError):
            request = failure.request
            self.logger.error('TimeoutError on %s', request.url)